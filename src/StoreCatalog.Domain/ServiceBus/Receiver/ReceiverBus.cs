using GeekBurger.Products.Contract;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.ServiceBus.Management;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using StoreCatalog.Domain.Enuns;
using StoreCatalog.Domain.Interfaces;
using StoreCatalog.Domain.Models.Area;
using StoreCatalog.Domain.ServiceBus.Topic;
using StoreCatalog.Domain.Suports.Options;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StoreCatalog.Domain.ServiceBus.Receiver
{
    public class ReceiverBus : IReceiverBus
    {
        private readonly IProductService _product;
        private readonly IAreaService _area;
        private readonly ITopicBus _topicBus;
        private readonly ServiceBusOption _option;
        private readonly RuleDescription _rule;

        public ReceiverBus(
            IProductService product,
            IAreaService area,
            ITopicBus topicBus,
            IOptions<ServiceBusOption> option)
        {
            _product = product;
            _area = area;
            _option = option.Value;
            _topicBus = topicBus;
            _rule = new RuleDescription
            {
                Filter = new CorrelationFilter { Label = _option.ServiceBus.Store },
                Name = "filter-store"
            };
        }

        public async Task ReceiverAsync(string topic, string filter, string subscription, TopicType type)
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

            if (type == TopicType.Product)
            {
                subscriptionClient.RegisterMessageHandler(HandleProduct, mo);
            }
            else
            {
                subscriptionClient.RegisterMessageHandler(HandleArea, mo);
            }
        }

        private Task HandleProduct(Message message, CancellationToken arg2)
        {
            var logs = new List<string>
            {
                $"message Label: {message.Label}",
                $"message CorrelationId: {message.CorrelationId}"
            };

            var json = Encoding.UTF8.GetString(message.Body);
            var product = JsonConvert.DeserializeObject<ProductToGet>(json);
            _product.Upsert(product);

            logs.Add($"Message Received");

            _topicBus.SendAsyn(_option.ServiceBus.TopicLog, logs);
            return Task.CompletedTask;
        }

        private Task HandleArea(Message message, CancellationToken arg2)
        {
            var logs = new List<string>
            {
                $"message Label: {message.Label}",
                $"message CorrelationId: {message.CorrelationId}"
            };

            var json = Encoding.UTF8.GetString(message.Body);
            var area = JsonConvert.DeserializeObject<AreasModel>(json);
            _area.Upsert(area);

            logs.Add($"Message Received");
            _topicBus.SendAsyn(_option.ServiceBus.TopicLog, logs);
            return Task.CompletedTask;
        }

        private Task ExceptionHandle(ExceptionReceivedEventArgs arg)
        {
            var logs = new List<string>
            {
                $"Message handler encountered an exception {arg.Exception}"
            };
            var context = arg.ExceptionReceivedContext;
            logs.Add($"- Endpoint: {context.Endpoint}, Path: {context.EntityPath}, Action: {context.Action}");
            _topicBus.SendAsyn(_option.ServiceBus.TopicLog, logs);

            return Task.CompletedTask;
        }

        private async Task CreateTopicAsync(string connectionString, string topic)
        {
            var client = new ManagementClient(connectionString);
            if (!await client.TopicExistsAsync(topic))
            {
                await client.CreateTopicAsync(topic);
                await _topicBus.SendAsyn(_option.ServiceBus.TopicLog, $"Create Topic {topic}");
            }
        }

        private async Task CreateSubscriptionAsync(string connectionString, string topic, string subscription)
        {
            var client = new ManagementClient(connectionString);
            if(!await client.SubscriptionExistsAsync(topic, subscription))
            {
                await client.CreateSubscriptionAsync(topic, subscription);
                await client.CreateRuleAsync(topic, subscription, _rule);

                await _topicBus.SendAsyn(_option.ServiceBus.TopicLog, $"Create Subscription {topic}");
            }
        }
    }
}
