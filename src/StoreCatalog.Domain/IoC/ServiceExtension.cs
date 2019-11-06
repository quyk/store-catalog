using Microsoft.Extensions.DependencyInjection;
using StoreCatalog.Domain.HttpClientFactory;
using StoreCatalog.Domain.Interfaces;
using StoreCatalog.Domain.Models.Area;
using StoreCatalog.Domain.Models.Product;
using StoreCatalog.Domain.Models.Store;
using StoreCatalog.Domain.ServiceBus;

namespace StoreCatalog.Domain.IoC
{
    public static class ServiceExtension
    {
        public static IServiceCollection UseServices(this IServiceCollection services)
        {
            services.AddTransient<IStoreCatalogClientFactory, StoreCatalogClientFactory>();
            services.AddSingleton<IAreaService, AreaService>();
            services.AddSingleton<IProductService, ProductService>();
            services.AddSingleton<IStoreService, StoreService>();

            services.AddTransient<IQueueBus, QueueBus>();
            return services;
        }
    }
}
