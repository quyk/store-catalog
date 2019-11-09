using Microsoft.Extensions.DependencyInjection;
using StoreCatalog.Domain.Enums;
using StoreCatalog.Domain.Extensions;
using StoreCatalog.Domain.Interfaces;
using StoreCatalog.Domain.ServiceBus.Topic;
using System.Threading.Tasks;

namespace StoreCatalog.Domain.IoC
{
    /// <summary>
    /// Startup required services to execute
    /// </summary>
    public static class ServiceStartExtension
    {
        /// <summary>
        /// Call the required services to initialize the StoreCatalog microservice
        /// </summary>
        /// <param name="services">IServiceCollection from Startup.cs</param>
        /// <returns>IServiceCollection</returns>
        public async static Task UseStart(this IServiceCollection services)
        {
            var serviceprovider = services.BuildServiceProvider();

            var storeService = serviceprovider.GetService<IStoreService>();
            var topicBus = serviceprovider.GetService<ITopicBus>();

            var store = await storeService.CheckStoreStatus();

            if (store != null)
                await topicBus.SendAsync(TopicType.StoreCatalogReady.GetDescription(), $"Store: {store.StoreId}. Status: {store.IsReady}");
        }
    }
}
