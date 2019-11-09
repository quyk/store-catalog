using Microsoft.Extensions.DependencyInjection;
using StoreCatalog.Domain.Interfaces;
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

            await storeService.CheckStoreStatus();
        }
    }
}
