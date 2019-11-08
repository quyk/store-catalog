using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreCatalog.Api.Models;
using StoreCatalog.Contract.Requests;
using StoreCatalog.Contract.Responses;
using StoreCatalog.Domain.Interfaces;
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

        public ProductController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        /// <summary>
        /// Retrieve all available products
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
        public async Task<ActionResult<IEnumerable<ProductResponse>>> GetProduct([FromQuery] ProductRequest productRequest)
        {
            try
            {
                var products = await _productService.GetProductsAsync(productRequest);

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
