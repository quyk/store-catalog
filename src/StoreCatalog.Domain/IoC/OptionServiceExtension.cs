using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StoreCatalog.Domain.Suports.Options;

namespace StoreCatalog.Domain.IoC
{
    /// <summary>
    /// Usefull TOptions configuration extensions
    /// </summary>
    public static class OptionServiceExtension
    {
        /// <summary>
        /// Configure all TOptions from appSettings.json
        /// </summary>
        /// <param name="services">IServiceCollection</param>
        /// <param name="configuration">IConfiguration</param>
        /// <returns>IServiceCollection</returns>
        public static IServiceCollection UseOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ServiceBusOption>(opt => configuration.GetSection("ServiceBus").Bind(opt));
            return services;
        }
    }
}
