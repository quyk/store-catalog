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

            var productService = serviceprovider.GetService<IProductService>();
            var areaService = serviceprovider.GetService<IAreaService>();

            await productService.GetProductsAsync();
            await areaService.GetAreaAsync();
        }
    }
}
