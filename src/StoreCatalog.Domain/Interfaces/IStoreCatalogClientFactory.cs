using System.Net.Http;

namespace StoreCatalog.Domain.Interfaces
{
    /// <summary>
    /// Interface to implement IHttpClientFactory and use on D.I
    /// </summary>
    public interface IStoreCatalogClientFactory : IHttpClientFactory { }
}
