using Microsoft.Extensions.DependencyInjection;
using StoreCatalog.Domain.Interfaces;
using StoreCatalog.Domain.Models.Area;
using StoreCatalog.Domain.Models.Product;

namespace StoreCatalog.Domain.IoC
{
    public static class ServiceExtension
    {
        public static IServiceCollection UseServices(this IServiceCollection service)
        {
           service.AddSingleton<IAreaService, AreaService>();
            service.AddSingleton<IProductService, ProductService>();

            return service;
        }
    }
}
