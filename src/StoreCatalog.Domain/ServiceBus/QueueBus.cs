using Microsoft.Azure.ServiceBus;
using StoreCatalog.Domain.Suports.Options;
using System.Text;
using System.Threading.Tasks;

namespace StoreCatalog.Domain.ServiceBus
{
    public class QueueBus : IQueueBus
    {
        private readonly ServiceBusOption _option;

        public QueueBus(ServiceBusOption option)
        {
            _option = option;
        }

        public async Task SendAsync(string message)
        {
            var queueClient = new QueueClient(_option.ConnectionString, "dd");
            var messages = new Message(Encoding.UTF8.GetBytes("mensagem"));
            await queueClient.SendAsync(messages);
            await queueClient.CloseAsync();
        }
    }
}
