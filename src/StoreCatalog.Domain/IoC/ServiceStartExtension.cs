using Microsoft.Extensions.DependencyInjection;
using StoreCatalog.Domain.Interfaces;
using StoreCatalog.Domain.ServiceBus.Topic;
using System.Threading.Tasks;

namespace StoreCatalog.Domain.IoC
{
    public static class ServiceStartExtension
    {
        public async static Task UseStart(this IServiceCollection services)
        {
            var serviceprovider = services.BuildServiceProvider();

            var storeService = serviceprovider.GetService<IStoreService>();
            var topicBus = serviceprovider.GetService<ITopicBus>();

            var store = await storeService.CheckStoreStatus();

            if (store != null)
                await topicBus.SendAsync("StoreCatalogReady", $"Store: {store.StoreId}. Status: {store.IsReady}");
        }
    }
}
