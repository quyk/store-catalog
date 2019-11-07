using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using StoreCatalog.Domain.Extensions;
using StoreCatalog.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace StoreCatalog.Domain.Models.Area
{
    public class AreaService : IAreaService
    {
        #region "  Properties  "

        private readonly string _baseUrl;
        private readonly string _cacheName = "areas";
        private readonly IStoreCatalogClientFactory _httpClientFactory;
        private readonly IMemoryCache _memoryCache;

        #endregion

        #region "  Constructor  "

        public AreaService(IStoreCatalogClientFactory httpClientFactory,
                           IConfiguration configuration,
                           IMemoryCache memoryCache)
        {
            _httpClientFactory = httpClientFactory;
            _baseUrl = configuration.GetValue<string>("AreaBaseUrl");
            _memoryCache = memoryCache;
        }

        #endregion

        #region "  IAreaService  "

        public async Task<IEnumerable<AreasModel>> GetAreaAsync()
        {
            if (!_memoryCache.TryGetValue(_cacheName, out Dictionary<Guid, AreasModel> areas))
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
                        var areasToGet = await response.Content.ReadAsJsonAsync<IEnumerable<AreasModel>>();

                        areas = areasToGet.ToDictionary(a => a.ProductionId, a => a);
                    }

                    if (null != areas)
                    {
                        _memoryCache.Set(_cacheName, areas, cacheOptions);
                    }
                }
            }

            return areas.Values;
        }


        public async Task Upsert(AreasModel area)
        {
            if (!_memoryCache.TryGetValue(_cacheName, out Dictionary<Guid, AreasModel> areas))
            {
                areas[area.ProductionId] = area;
            }
            else
            {
                await GetAreaAsync();
            }
        }
        #endregion
    }
}
