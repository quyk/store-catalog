using StoreCatalog.Domain.Enums;
using System.Threading.Tasks;

namespace StoreCatalog.Domain.ServiceBus.Receiver
{
    public interface IReceiverBus
    {
        Task ReceiverAsync(string topicName, string filter, string subscription, TopicType type);
    }
}
