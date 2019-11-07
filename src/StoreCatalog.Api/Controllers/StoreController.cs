using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreCatalog.Api.Models;
using StoreCatalog.Contract;
using StoreCatalog.Domain.Interfaces;
using System;
using System.Threading.Tasks;

namespace StoreCatalog.Api.Controllers
{
    [Route("api/store")]
    [Produces("application/json")]
    public class StoreController : ControllerBase
    {
        private readonly IStoreService _storeService;

        public StoreController(IStoreService storeService)
        {
            _storeService = storeService;
        }

        /// <summary>
        /// Method to retrieve Store status
        /// </summary>
        /// <remarks>
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
        public async Task<ActionResult<StoreModel>> GetStore()
        {
            try
            {
                var store = await _storeService.CheckStoreStatus();

                if (store != null)
                    return Ok(store);
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
