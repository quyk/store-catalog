using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using StoreCatalog.Domain.Suports.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreCatalog.Domain.ServiceBus.Topic
{
    public class TopicBus : ITopicBus
    {
        private readonly ServiceBusOption _option;

        public TopicBus(IOptions<ServiceBusOption> option)
        {
            _option = option.Value;
        }

        public async Task SendAsyn(string topic, string message)
        {
            var topicClient = new TopicClient(_option.ConnectionString, topic);
            await topicClient.SendAsync(new Message(Encoding.UTF8.GetBytes(message)));
            await topicClient.CloseAsync();
        }

        public async Task SendAsyn(string topic, IList<string> message)
        {
            var topicClient = new TopicClient(_option.ConnectionString, topic);
            var json = JsonConvert.SerializeObject(message);
            await topicClient.SendAsync(message.Select(m => new Message(Encoding.UTF8.GetBytes(m))).ToList());
            await topicClient.CloseAsync();
        }
    }
}
