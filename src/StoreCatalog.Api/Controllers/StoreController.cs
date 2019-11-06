using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreCatalog.Api.Models;
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
        /// <returns>A StoreModel entity</returns>
        /// <response code="200">Returns a StoreModel</response>
        /// <response code="404">When none store was found</response>
        /// <response code="400">When some error occours</response>
        [HttpGet]
        [ProducesResponseType(typeof(OkObjectResult), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(BadRequestObjectResult))]
        public async Task<ActionResult<StoreModel>> GetStore()
        {
            var store = new StoreModel
            {
                StoreId = new Guid("FC7DE87A-741F-4558-93C9-3A14CC3B22E8"),
                Ready = true
            };

            return new OkObjectResult(store);
        }
    }
}
