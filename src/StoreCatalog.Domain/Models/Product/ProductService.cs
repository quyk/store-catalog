using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using StoreCatalog.Contract.Responses;
using StoreCatalog.Domain.Extensions;
using StoreCatalog.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Linq;
using GeekBurger.Products.Contract;

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
            if (!_memoryCache.TryGetValue(_cacheName, out IEnumerable<ProductToGet> products))
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
                        products = await response.Content.ReadAsJsonAsync<IEnumerable<ProductToGet>>();
                    }

                    if (null != products &&
                        products.Count() > 0)
                    {
                        _memoryCache.Set(_cacheName, products, cacheOptions);
                    }
                }
            }

            return products;
        }
    }
}
