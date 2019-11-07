using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.ServiceBus.Management;
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
        private readonly RuleDescription _rule;

        public ReceiverBus(ServiceBusOption option)
        {
            _option = option;
            _rule = new RuleDescription
            {
                Filter = new CorrelationFilter { Label = _option.ServiceBus.Store },
                Name = "filter-store"
            };
        }

        public async Task ReceiverAsync(string topic, string filter, string subscription)
        {
            await CreateTopicAsync(_option.ConnectionString, topic);
            await CreateSubscriptionAsync(_option.ConnectionString, topic, subscription);

            var subscriptionClient = new SubscriptionClient(_option.ConnectionString, topic, subscription);

            var rules = await subscriptionClient.GetRulesAsync();

            if (rules.Any(r => r.Name.Equals("$Default")))
            {
                //by default a 1=1 rule is added when subscription is created, so we need to remove it
                await subscriptionClient.RemoveRuleAsync("$Default");
            }

            if (!rules.Any(r => r.Name.Equals("filter-store")))
            {
                await subscriptionClient.AddRuleAsync(_rule);
            }

            var mo = new MessageHandlerOptions(ExceptionHandle)
            {
                MaxConcurrentCalls = 5,
                AutoComplete = false
            };

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

        private async Task CreateTopicAsync(string connectionString, string topic)
        {
            var client = new ManagementClient(connectionString);
            if (!await client.TopicExistsAsync(topic))
            {
                await client.CreateTopicAsync(topic);
            }
        }

        private async Task CreateSubscriptionAsync(string connectionString, string topic, string subscription)
        {
            var client = new ManagementClient(connectionString);
            if(!await client.SubscriptionExistsAsync(topic, subscription))
            {
                await client.CreateSubscriptionAsync(topic, subscription);
                await client.CreateRuleAsync(topic, subscription, _rule);
            }
        }
    }
}
