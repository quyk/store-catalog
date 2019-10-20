using Microsoft.AspNetCore.Mvc;
using StoreCatalog.Api.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StoreCatalog.Api.Controllers
{
    [Route("api/    ")]
    public class ProductionController : Controller
    {
        [HttpGet("areas")]
        public async Task<ActionResult<AreasModel>> GetAreas()
        {
            var area = new AreasModel
            {
                ProductionId = new Guid("28AAE9F5-F9F2-499F-AB2A-134EAF42CB19"),
                Restrictions = new List<string>
                {
                    "soy",
                    "dairy",
                    "gluten",
                    "peanut"
                },
                On = true
            };
            return new OkObjectResult(area);
        }
    }
}
