using GeekBurger.Products.Contract;
using Microsoft.Extensions.Caching.Memory;
using StoreCatalog.Contract.Requests;
using StoreCatalog.Domain.Enums;
using StoreCatalog.Domain.Extensions;
using StoreCatalog.Domain.Interfaces;
using StoreCatalog.Domain.ServiceBus.Topic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace StoreCatalog.Domain.Models.Product
{
    public class ProductService : IProductService
    {
        #region "  Properties  "

        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IMemoryCache _memoryCache;
        private readonly ITopicBus _topicBus;
        private readonly string _cacheName = "products";

        #endregion

        #region "  Constructor  "

        public ProductService(IHttpClientFactory httpClientFactory,
            IMemoryCache memoryCache,
            ITopicBus topicBus)
        {
            _httpClientFactory = httpClientFactory;
            _memoryCache = memoryCache;
            _topicBus = topicBus;
        }

        #endregion

        #region "  IProductService  "

        public async Task<IEnumerable<ProductToGet>> GetProductsAsync(ProductRequest productRequest)
        {
            var cacheOptions = new MemoryCacheEntryOptions()
            {
                AbsoluteExpiration = DateTime.Now.AddHours(6)
            };

            if (!_memoryCache.TryGetValue(_cacheName, out Dictionary<Guid, ProductToGet> products))
            {
                using (var httpClient = _httpClientFactory.CreateClient("Products"))
                {
                    var response = await httpClient.GetAsync($"api/products?storeName={productRequest.StoreName}");

                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        var productsToGet = await response.Content.ReadAsJsonAsync<IEnumerable<ProductToGet>>();

                        products = productsToGet.ToDictionary(p => p.ProductId, p => p);
                    }

                    if (null != products && products.Count > 0)
                    {
                        _memoryCache.Set(_cacheName, products, cacheOptions);
                    }
                }
            }

            if (productRequest.Restrictions?.Count > 0)
            {
                using (var httpClient = _httpClientFactory.CreateClient("Ingredients"))
                {
                    var storeId = products.Values.FirstOrDefault().StoreId;

                    var restrictionsParameters = string.Empty;

                    foreach (var restriction in productRequest.Restrictions)
                    {
                        restrictionsParameters += $"&Restrictions={restriction}";
                    }

                    var response = await httpClient.GetAsync($"api/products/byrestrictions/?StoreId=${storeId}{restrictionsParameters}");

                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        var productsRestriction = await response.Content.ReadAsJsonAsync<IEnumerable<ProductRestrictionResponse>>();

                        foreach (var product in productsRestriction)
                        {
                            var p = products[product.ProductId];

                            p.Items = product.Ingredients.Select(i => new ItemToGet() { Name = i }).ToList();

                            products[product.ProductId] = p;
                        }

                        _memoryCache.Set(_cacheName, products, cacheOptions);

                        var productsId = productsRestriction.Select(p => p.ProductId);

                        await _topicBus.SendAsync(TopicType.UserWithLessOffer.GetDescription(), "Products with restriction find!");

                        return products.Where(p => productsId.Contains(p.Key)).Select(v => v.Value);
                    }
                }
            }

            return products.Values;
        }

        public async Task Upsert(ProductToGet product)
        {
            if (!_memoryCache.TryGetValue(_cacheName, out Dictionary<Guid, ProductToGet> products))
            {
                products[product.ProductId] = product;
            }

            await Task.CompletedTask;
        }

        #endregion
    }
}
