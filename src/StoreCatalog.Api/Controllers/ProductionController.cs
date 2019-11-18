using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreCatalog.Contract.Responses;
using StoreCatalog.Domain.Enums;
using StoreCatalog.Domain.Interfaces;
using StoreCatalog.Domain.ServiceBus.Topic;
using System;
using System.Threading.Tasks;

namespace StoreCatalog.Api.Controllers
{
    [Route("api/production")]
    [Produces("application/json")]
    public class ProductionController : Controller
    {
        private readonly IAreaService _areaService;
        private readonly IMapper _mapper;
        private readonly ITopicBus _topicBus;

        public ProductionController(IAreaService areaService, IMapper mapper, ITopicBus topicBus)
        {
            _areaService = areaService;
            _mapper = mapper;
            _topicBus = topicBus;
        }

        /// <summary>
        /// Retrieve all available areas
        /// </summary>
        /// <returns>A AreasResponse entity</returns>
        /// <remarks>
        /// Example:
        /// 
        ///     GET - api/production/areas
        /// 
        /// </remarks>
        /// <response code="200">Returns a <see cref="AreasResponse"/></response>
        /// <response code="404">When none area was found</response>
        /// <response code="400">When some error occours</response>
        [HttpGet("areas")]
        [ProducesResponseType(typeof(AreasResponse), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(BadRequestObjectResult))]
        public async Task<ActionResult> GetAreasAsync()
        {
            try
            {
                await _topicBus.SendAsync(TopicType.Log.ToString(), "Calling Get Areas..");

                var response = await _areaService.GetAreaAsync();

                if (response != null)
                {
                    return Ok(_mapper.Map<AreasResponse>(response));
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
                await _topicBus.SendAsync(TopicType.Log.ToString(), "Returning Get Areas..");
            }
        }
    }
}
