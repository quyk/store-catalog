using System.Threading.Tasks;

namespace StoreCatalog.Domain.ServiceBus
{
    public interface IQueueBus
    {
        Task SendAsync(string message);
    }
}