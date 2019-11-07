using System.Collections.Generic;
using System.Threading.Tasks;

namespace StoreCatalog.Domain.ServiceBus.Topic
{
    public interface ITopicBus
    {
        Task SendAsyn(string topic, string message);
        Task SendAsyn(string topic, IList<string> message);
    }
}
