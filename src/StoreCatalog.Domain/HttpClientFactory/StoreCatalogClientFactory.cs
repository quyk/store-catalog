using StoreCatalog.Domain.Interfaces;
using System.Net.Http;

namespace StoreCatalog.Domain.HttpClientFactory
{
    public class StoreCatalogClientFactory : IStoreCatalogClientFactory
    {
        public HttpClient CreateClient(string name)
        {
            return new HttpClient();
        }
    }
}
