using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StoreCatalog.Api.Models;
using StoreCatalog.Contract.Responses;
using StoreCatalog.Domain.Interfaces;
using System;
using System.Threading.Tasks;

namespace StoreCatalog.Api.Controllers
{
    [Route("api/production")]
    public class ProductionController : Controller
    {
        private readonly IAreaService _areaService;
        private readonly IMapper _mapper;

        public ProductionController(IAreaService areaService, IMapper mapper)
        {
            _areaService = areaService;
            _mapper = mapper;
        }

        [HttpGet("areas")]
        public async Task<ActionResult<AreasModel>> GetAreas()
        {
            try
            {
                var response = await _areaService.GetAreaAsync();

                if (response != null)
                    return Ok(_mapper.Map<AreasResponse>(response));
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
