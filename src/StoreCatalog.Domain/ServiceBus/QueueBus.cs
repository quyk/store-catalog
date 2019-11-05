using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Options;
using StoreCatalog.Domain.Suports.Options;
using System.Text;
using System.Threading.Tasks;

namespace StoreCatalog.Domain.ServiceBus
{
    public class QueueBus : IQueueBus
    {
        private readonly ServiceBusOption _option;

        public QueueBus(IOptions<ServiceBusOption> option)
        {
            _option = option.Value;
        }

        public async Task SendAsync(string message)
        {
            var queueClient = new QueueClient(_option.ConnectionString, _option.QueueTest);
            var messages = new Message(Encoding.UTF8.GetBytes("mensagem"));
            await queueClient.SendAsync(messages);
            await queueClient.CloseAsync();
        }
    }
}
