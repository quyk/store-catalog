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

namespace StoreCatalog.Domain.Models.Area
{
    public class AreaService : IAreaService
    {
        private readonly string _baseUrl;
        private readonly IStoreCatalogClientFactory _httpClientFactory;
        private readonly IMemoryCache _memoryCache;
        private readonly string _cacheName = "areas";

        public AreaService(IStoreCatalogClientFactory httpClientFactory, 
                           IConfiguration configuration,
                           IMemoryCache memoryCache)
        {
            _httpClientFactory = httpClientFactory;
            _baseUrl = configuration.GetValue<string>("AreaBaseUrl");
            _memoryCache = memoryCache;
        }

        public async Task<AreasModel> GetAreaAsync() 
        {
            if (!_memoryCache.TryGetValue(_cacheName, out AreasModel area))
            {
                var cacheOptions = new MemoryCacheEntryOptions()
                {
                    AbsoluteExpiration = DateTime.Now.AddHours(6)
                };

                using (var httpClient = _httpClientFactory.CreateClient("Areas"))
                {
                    var response = await httpClient.GetAsync($"{_baseUrl}/api/production/areas");

                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        area = await response.Content.ReadAsJsonAsync<AreasModel>();
                    }

                    if (null != area)
                    {
                        _memoryCache.Set(_cacheName, area, cacheOptions);
                    }
                }
            }
            return area;
        }
    }
}
