using Microsoft.Azure.ServiceBus;
using StoreCatalog.Domain.Suports.Options;
using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StoreCatalog.Domain.ServiceBus.Receiver
{
    public class ReceiverBus : IReceiverBus
    {
        private readonly ServiceBusOption _option;

        public ReceiverBus(ServiceBusOption option)
        {
            _option = option;
        }

        public async Task ReceiverAsync(string topicName, string filter, string subscription)
        {
            var subscriptionClient = new SubscriptionClient(_option.ConnectionString, topicName, subscription);

            var rules = await subscriptionClient.GetRulesAsync();

            if (rules.Any(r => r.Name.Equals("$Default")))
            {
                //by default a 1=1 rule is added when subscription is created, so we need to remove it
                await subscriptionClient.RemoveRuleAsync("$Default");
            }

            if (!rules.Any(r => r.Name.Equals("filter-store")))
            {
                await subscriptionClient.AddRuleAsync(new RuleDescription
                {
                    Filter = new CorrelationFilter { Label = filter },
                    Name = "filter-store"
                });
            }

            var mo = new MessageHandlerOptions(ExceptionHandle) { AutoComplete = true };

            subscriptionClient.RegisterMessageHandler(Handle, mo);            
        }

        private static Task Handle(Message message, CancellationToken arg2)
        {
            Console.WriteLine($"message Label: {message.Label}");
            Console.WriteLine($"message CorrelationId: {message.CorrelationId}");
            var productChangesString = Encoding.UTF8.GetString(message.Body);

            Console.WriteLine("Message Received");
            Console.WriteLine(productChangesString);

            return Task.CompletedTask;
        }

        private static Task ExceptionHandle(ExceptionReceivedEventArgs arg)
        {
            Console.WriteLine($"Message handler encountered an exception {arg.Exception}.");
            var context = arg.ExceptionReceivedContext;
            Console.WriteLine($"- Endpoint: {context.Endpoint}, Path: {context.EntityPath}, Action: {context.Action}");
            return Task.CompletedTask;
        }
    }
}
