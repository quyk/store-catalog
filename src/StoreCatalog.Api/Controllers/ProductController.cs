using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using StoreCatalog.Contract.Responses;
using StoreCatalog.Domain.Interfaces;
using StoreCatalog.Domain.ServiceBus.Topic;
using StoreCatalog.Domain.Suports.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StoreCatalog.Api.Controllers
{
    [Route("api/products")]
    [ApiController]
    [Produces("application/json")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductController(IProductService productService, 
                                 IMapper mapper,
                                 ITopicBus topicBus,
                                 IOptions<ServiceBusOption> option)
        {
            _productService = productService;
            _mapper = mapper;
        }

        /// <summary>
        /// Method to retrieve all available products
        /// </summary>
        /// <remarks>
        /// Example:
        /// 
        ///     GET - api/products
        /// 
        /// </remarks>
        /// <returns>A list of <see cref="ProductResponse"/></returns>
        /// <response code="200">Returns a list of products</response>
        /// <response code="404">When none product was found</response>
        /// <response code="400">When some error occours</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ProductResponse>), StatusCodes.Status200OK)]        
        [ProducesErrorResponseType(typeof(BadRequestObjectResult))]
        public async Task<ActionResult<ProductResponse>> GetProduct()
        {
            try
            {
                var products = await _productService.GetProductsAsync();

                if (products != null)
                    return Ok(_mapper.Map<IEnumerable<ProductResponse>>(products));
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
