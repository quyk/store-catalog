using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StoreCatalog.Domain.Suports.Options;

namespace StoreCatalog.Domain.IoC
{
    public static class OptionServiceExtension
    {
        public static IServiceCollection UseOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ServiceBusOption>(opt => configuration.GetSection("ServiceBus").Bind(opt));
            return services;
        }
    }
}
