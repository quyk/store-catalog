﻿using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using StoreCatalog.Contract.Responses;
using StoreCatalog.Domain.Extensions;
using StoreCatalog.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace StoreCatalog.Domain.Models.Area
{
    public class AreaService : IAreaService
    {
        private readonly string _baseUrl;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IMemoryCache _memoryCache;
        private readonly string _cacheName = "areas";

        public AreaService(IHttpClientFactory httpClientFactory, 
                           IConfiguration configuration,
                           IMemoryCache memoryCache)
        {
            _httpClientFactory = httpClientFactory;
            _baseUrl = configuration.GetValue<string>("AreaBaseUrl");
            _memoryCache = memoryCache;
        }

        public async Task<IEnumerable<AreasResponse>> GetAreaAsync() 
        {

            if (!_memoryCache.TryGetValue(_cacheName, out IEnumerable<AreasResponse> areas))
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
                        areas = await response.Content.ReadAsJsonAsync<IEnumerable<AreasResponse>>();
                    }

                    _memoryCache.Set(_cacheName, areas, cacheOptions);
                }
            }

            return areas;

        }
    }
}
