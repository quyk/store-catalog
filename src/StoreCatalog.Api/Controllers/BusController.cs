using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using StoreCatalog.Api.Models;
using StoreCatalog.Domain.ServiceBus;
using StoreCatalog.Domain.ServiceBus.Topic;
using StoreCatalog.Domain.Suports.Options;
using System.Threading.Tasks;

namespace StoreCatalog.Api.Controllers
{
    [Route("api/store")]
    public class BusController : Controller
    {
        private readonly IQueueBus _queueBus;

        public BusController(IQueueBus queueBus,
                             ITopicBus topicBus,
                             IOptions<ServiceBusOption> option)
        {
            _queueBus = queueBus;
        }

        [HttpPost]
        public async Task<ActionResult> SendAsync([FromBody] BusModel model)
        {
            await _queueBus.SendAsync(model.Message);
            return Created("", null);
        }
    }
}
