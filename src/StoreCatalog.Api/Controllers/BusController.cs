using Microsoft.AspNetCore.Mvc;
using StoreCatalog.Api.Models;
using StoreCatalog.Domain.ServiceBus;
using System.Threading.Tasks;

namespace StoreCatalog.Api.Controllers
{
    [Route("api/store")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class BusController : Controller
    {
        private readonly IQueueBus _queueBus;

        public BusController(IQueueBus queueBus)
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
