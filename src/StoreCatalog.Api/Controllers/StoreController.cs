using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreCatalog.Contract;
using StoreCatalog.Domain.Enums;
using StoreCatalog.Domain.Interfaces;
using StoreCatalog.Domain.ServiceBus.Topic;
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

        public StoreController(IStoreService storeService,
                               ITopicBus topicBus)
        {
            _storeService = storeService;
            _topicBus = topicBus;
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
        public async Task<ActionResult> GetStoreAsync()
        {
            try
            {
                await _topicBus.SendAsync(TopicType.Log.ToString(), "Calling Get Store..");

                var store = await _storeService.CheckStoreStatus();

                if (store != null)
                {
                    return Ok(store);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                await _topicBus.SendAsync(TopicType.Log.ToString(), ex.ToString());

                return BadRequest(ex);
            }
            finally
            {
                await _topicBus.SendAsync(TopicType.Log.ToString(), "Returning Get Store..");
            }
        }
    }
}
