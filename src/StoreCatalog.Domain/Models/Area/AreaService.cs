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

        public async Task<IEnumerable<AreasModel>> GetAreaAsync() 
        {
            if (!_memoryCache.TryGetValue(_cacheName, out IEnumerable<AreasModel> areas))
            {
                var cacheOptions = new MemoryCacheEntryOptions()
                {
                    AbsoluteExpiration = DateTime.Now.AddHours(6)
                };

                using (var httpClient = _httpClientFactory.CreateClient())
                {
                    var response = await httpClient.GetAsync(_baseUrl);

                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        areas = await response.Content.ReadAsJsonAsync<IEnumerable<AreasModel>>();
                    }

                    if (null != areas ||
                        areas.Count() > 0)
                    {
                        _memoryCache.Set(_cacheName, areas, cacheOptions);
                    }
                }
            }
            return areas;
        }
    }
}
