using Microsoft.Extensions.DependencyInjection;
using StoreCatalog.Domain.HttpClientFactory;
using StoreCatalog.Domain.Interfaces;
using StoreCatalog.Domain.Models.Area;
using StoreCatalog.Domain.Models.Product;

namespace StoreCatalog.Domain.IoC
{
    public static class ServiceExtension
    {
        public static IServiceCollection UseServices(this IServiceCollection services)
        {
            services.AddTransient<IStoreCatalogClientFactory, StoreCatalogClientFactory>();
            services.AddSingleton<IAreaService, AreaService>();
            services.AddSingleton<IProductService, ProductService>();

            return services;
        }
    }
}
