using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Options;
using StoreCatalog.Domain.Suports.Options;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StoreCatalog.Domain.ServiceBus.Receiver
{
    public class ReceiverBus : IReceiverBus
    {
        private readonly ServiceBusOption _option;

        public ReceiverBus(IOptions<ServiceBusOption> option)
        {
            _option = option.Value;
        }

        public async Task ReceiverAsync(string topicName, string filter, string subscription)
        {
            var subscriptionClient = new SubscriptionClient(_option.ConnectionString, topicName, subscription);

            //by default a 1=1 rule is added when subscription is created, so we need to remove it
            await subscriptionClient.RemoveRuleAsync("$Default");

            await subscriptionClient.AddRuleAsync(new RuleDescription
            {
                Filter = new CorrelationFilter { Label = filter },
                Name = "filter-store"
            });

            var mo = new MessageHandlerOptions(ExceptionHandle) { AutoComplete = true };

            subscriptionClient.RegisterMessageHandler(Handle, mo);

            Console.ReadLine();
        }

        private static Task Handle(Message message, CancellationToken arg2)
        {
            Console.WriteLine($"message Label: {message.Label}");
            Console.WriteLine($"message CorrelationId: {message.CorrelationId}");
            var productChangesString = Encoding.UTF8.GetString(message.Body);

            Console.WriteLine("Message Received");
            Console.WriteLine(productChangesString);

            //Thread.Sleep(40000);

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
