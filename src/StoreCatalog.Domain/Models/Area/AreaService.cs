using Microsoft.Extensions.Configuration;
using StoreCatalog.Contract.Responses;
using StoreCatalog.Domain.Extensions;
using StoreCatalog.Domain.Interfaces;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace StoreCatalog.Domain.Models.Area
{
    public class AreaService : IAreaService
    {
        private readonly string _baseUrl;
        private readonly IHttpClientFactory _httpClientFactory;

        public AreaService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _baseUrl = configuration.GetValue<string>("AreaBaseUrl");
        }

        public async Task<AreasResponse> GetAreaAsync() 
        {
            using (var httpClient = _httpClientFactory.CreateClient())
            {
                var response = await httpClient.GetAsync(_baseUrl);

                if (response.StatusCode == HttpStatusCode.OK)
                    return await response.Content.ReadAsJsonAsync<AreasResponse>();
                else
                    return null;
            }
        }
    }
}
