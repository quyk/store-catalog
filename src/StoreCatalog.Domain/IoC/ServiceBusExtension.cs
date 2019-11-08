using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using StoreCatalog.Domain.Enums;
using StoreCatalog.Domain.ServiceBus.Receiver;
using StoreCatalog.Domain.Suports.Options;
using System.Threading.Tasks;

namespace StoreCatalog.Domain.IoC
{
    public static class ServiceBusExtension
    {
        public static async Task<IServiceCollection> UseServiceBus(this IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            var receiver = serviceProvider.GetService<IReceiverBus>();

            var option = serviceProvider.GetService<IOptions<ServiceBusOption>>();
            var sb = option.Value;

            await receiver.ReceiverAsync(sb.ServiceBus.Product.Topic, sb.ServiceBus.Store, sb.ServiceBus.Product.Subscription, TopicType.Product);
            await receiver.ReceiverAsync(sb.ServiceBus.ProductionArea.Topic, sb.ServiceBus.Store, sb.ServiceBus.ProductionArea.Subscription, TopicType.ProductionArea);

            return services;
        }
    }
}
