using Microsoft.Extensions.DependencyInjection;
using StoreCatalog.Domain.Interfaces;
using System.Threading.Tasks;

namespace StoreCatalog.Domain.IoC
{
    public static class ServiceStartExtension
    {
        public async static Task UseStart(this IServiceCollection services)
        {
            var serviceprovider = services.BuildServiceProvider();

            var storeService = serviceprovider.GetService<IStoreService>();

            //var store = await storeService.CheckStoreStatus();

            //if (store != null)
                //TODO publicar no tópico StoreCatalogReady
        }
    }
}
