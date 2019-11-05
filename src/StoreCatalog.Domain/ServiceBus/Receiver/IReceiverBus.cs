using System.Threading.Tasks;

namespace StoreCatalog.Domain.ServiceBus.Receiver
{
    interface IReceiverBus
    {
        Task ReceiverAsync(string topicName, string filter, string subscription);
    }
}
