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

namespace StoreCatalog.Domain.Models.Product
{
    public class ProductService : IProductService
    {
        private readonly string _baseUrl;
        private readonly IStoreCatalogClientFactory _httpClientFactory;
        private readonly IMemoryCache _memoryCache;

        public ProductService(IStoreCatalogClientFactory httpClientFactory,
            IConfiguration configuration,
            IMemoryCache memoryCache)
        {
            _httpClientFactory = httpClientFactory;
            _baseUrl = configuration.GetValue<string>("ProductBaseUrl");
            _memoryCache = memoryCache;
        }

        public async Task<IEnumerable<ProductResponse>> GetProductsAsync()
        {
            var cacheName = "products";

            if (!_memoryCache.TryGetValue(cacheName, out IEnumerable<ProductResponse> products))
            {
                var cacheOptions = new MemoryCacheEntryOptions()
                {
                    AbsoluteExpiration = DateTime.Now.AddHours(6)
                };

                using (var httpClient = _httpClientFactory.CreateClient())
                {
                    var response = await httpClient.GetAsync($"{_baseUrl}/api/products?storeName=Los%20Angeles%20-%20Pasadena");

                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        products = await response.Content.ReadAsJsonAsync<IEnumerable<ProductResponse>>();
                    }

                    if (null != products &&
                        products.Count() > 0)
                    {
                        _memoryCache.Set(cacheName, products, cacheOptions);
                    }
                }
            }

            return products;
        }
    }
}
