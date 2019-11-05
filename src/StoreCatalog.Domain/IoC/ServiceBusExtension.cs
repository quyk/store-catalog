using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using StoreCatalog.Domain.ServiceBus.Receiver;
using StoreCatalog.Domain.Suports.Options;
using System.Threading.Tasks;

namespace StoreCatalog.Domain.IoC
{
    public static class ServiceBusExtension
    {
        const string Filter = "LOS ANGELES - PASADENA";

        public static async Task<IServiceCollection> UseServiceBus(this IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            var option = serviceProvider.GetService<IOptions<ServiceBusOption>>();

            var receiver = new ReceiverBus(option);

            //Upsert
            await receiver.ReceiverAsync("productchanged", Filter, "StoreCatalog-ProductChanged");

            await receiver.ReceiverAsync("productionareachanged", Filter, "StoreCatalog-ProductionAreaChanged");

            return services;
        }
    }
}
