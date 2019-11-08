using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using StoreCatalog.Domain.Suports.Options;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreCatalog.Domain.ServiceBus.Topic
{
    public class TopicBus : ITopicBus
    {
        #region "  Properties  "

        private readonly ServiceBusOption _option;

        #endregion

        #region "  Constructor  "

        public TopicBus(IOptions<ServiceBusOption> option)
        {
            _option = option.Value;
        }

        #endregion

        #region "  ITopicBus  "

        public async Task SendAsync(string topic, string message)
        {
            var topicClient = new TopicClient(_option.ConnectionString, topic);
            await topicClient.SendAsync(new Message(Encoding.UTF8.GetBytes(message)));
            await topicClient.CloseAsync();
        }

        public async Task SendAsync(string topic, IList<string> messages)
        {
            var topicClient = new TopicClient(_option.ConnectionString, topic);
            var json = JsonConvert.SerializeObject(messages);
            await topicClient.SendAsync(messages.Select(m => new Message(Encoding.UTF8.GetBytes(m))).ToList());
            await topicClient.CloseAsync();
        }

        #endregion
    }
}
