using GeekBurger.Products.Contract;
using Microsoft.Extensions.Caching.Memory;
using StoreCatalog.Domain.Extensions;
using StoreCatalog.Domain.Interfaces;
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
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IMemoryCache _memoryCache;
        private readonly string _cacheName = "products";

        public ProductService(IHttpClientFactory httpClientFactory,
            IMemoryCache memoryCache)
        {
            _httpClientFactory = httpClientFactory;
            _memoryCache = memoryCache;
        }

        public async Task<IEnumerable<ProductToGet>> GetProductsAsync()
        {
            if (!_memoryCache.TryGetValue(_cacheName, out Dictionary<Guid, ProductToGet> products))
            {
                var cacheOptions = new MemoryCacheEntryOptions()
                {
                    AbsoluteExpiration = DateTime.Now.AddHours(6)
                };

                using (var httpClient = _httpClientFactory.CreateClient("Products"))
                {
                    var response = await httpClient.GetAsync($"api/products?storeName=Los%20Angeles%20-%20Pasadena");

                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        var productsToGet = await response.Content.ReadAsJsonAsync<IEnumerable<ProductToGet>>();

                        products = productsToGet.ToDictionary(p => p.ProductId, p => p);
                    }

                    if (null != products &&
                        products.Count > 0)
                    {
                        _memoryCache.Set(_cacheName, products, cacheOptions);
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
            else
            {
                await GetProductsAsync();
            }
        }
    }
}
