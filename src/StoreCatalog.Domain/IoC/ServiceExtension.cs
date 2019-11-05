using Microsoft.Extensions.DependencyInjection;
using StoreCatalog.Domain.HttpClientFactory;
using StoreCatalog.Domain.Interfaces;
using StoreCatalog.Domain.Models.Area;
using StoreCatalog.Domain.Models.Product;
using StoreCatalog.Domain.ServiceBus;
using System.Threading.Tasks;

namespace StoreCatalog.Domain.IoC
{
    public static class ServiceExtension
    {
        public async static Task<IServiceCollection> UseServices(this IServiceCollection services)
        {
            services.AddTransient<IStoreCatalogClientFactory, StoreCatalogClientFactory>();
            services.AddSingleton<IAreaService, AreaService>();
            services.AddSingleton<IProductService, ProductService>();

            var serviceprovider = services.BuildServiceProvider();

            var productService = serviceprovider.GetService<ProductService>();
            var areaService = serviceprovider.GetService<AreaService>();

            await productService.GetProductsAsync();
            await areaService.GetAreaAsync();

            //TODO: Publicar StoreCatalogReady


            services.AddTransient<IQueueBus, QueueBus>();
            return services;
        }
    }
}
