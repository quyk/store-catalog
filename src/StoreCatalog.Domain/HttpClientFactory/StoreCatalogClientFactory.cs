using StoreCatalog.Domain.Interfaces;
using System.Net.Http;

namespace StoreCatalog.Domain.HttpClientFactory
{
    /// <summary>
    /// HttpClientFactory concrete implementation because we can't add a <see cref="IHttpClientFactory"/> on the D.I
    /// </summary>
    public class StoreCatalogClientFactory : IStoreCatalogClientFactory
    {
        public HttpClient CreateClient(string name)
        {
            return new HttpClient();
        }
    }
}
