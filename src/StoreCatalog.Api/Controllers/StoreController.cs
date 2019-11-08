using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using StoreCatalog.Api.Models;
using StoreCatalog.Contract;
using StoreCatalog.Domain.Interfaces;
using StoreCatalog.Domain.ServiceBus.Topic;
using StoreCatalog.Domain.Suports.Options;
using System;
using System.Threading.Tasks;

namespace StoreCatalog.Api.Controllers
{
    [Route("api/store")]
    [Produces("application/json")]
    public class StoreController : ControllerBase
    {
        private readonly IStoreService _storeService;
        private readonly ITopicBus _topicBus;
        private readonly IOptions<ServiceBusOption> _option;

        public StoreController(IStoreService storeService,
                               ITopicBus topicBus,
                               IOptions<ServiceBusOption> option)
        {
            _storeService = storeService;
            _topicBus = topicBus;
            _option = option;
        }

        /// <summary>
        /// Retrieve Store status
        /// </summary>
        /// <remarks>
        /// Example:
        /// 
        ///     GET - api/store
        /// 
        /// </remarks>
        /// <returns>A <see cref="Ready"/> entity</returns>
        /// <response code="200">Returns a <see cref="Ready"/></response>
        /// <response code="404">When none store was found</response>
        /// <response code="400">When some error occours</response>
        [HttpGet]
        [ProducesResponseType(typeof(Ready), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(BadRequestObjectResult))]
        public async Task<ActionResult<Ready>> GetStore()
        {
            try
            {
                await _topicBus.SendAsync(_option.Value.ServiceBus.TopicLog, "Calling Get Store..");

                var store = await _storeService.CheckStoreStatus();

                if (store != null)
                    return Ok(store);
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                await _topicBus.SendAsync(_option.Value.ServiceBus.TopicLog, ex.ToString());
                return BadRequest(ex);
            }
            finally
            {
                await _topicBus.SendAsync(_option.Value.ServiceBus.TopicLog, "Returning Get Store..");
            }
        }
    }
}
