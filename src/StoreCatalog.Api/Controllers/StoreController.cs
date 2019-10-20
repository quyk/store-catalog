using Microsoft.AspNetCore.Mvc;
using StoreCatalog.Api.Models;
using System;
using System.Threading.Tasks;

namespace StoreCatalog.Api.Controllers
{
    [Route("api/store")]
    public class StoreController : Controller
    {
        [HttpGet]
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
