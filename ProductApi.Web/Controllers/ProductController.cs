using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProductApi.BusinessLayer.Interfaces;
using ProductApi.Core.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductApi.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : BaseController
    {
        private readonly IProductService _productService;
        public ProductController(ILogger<ProductController> logger,
            IProductService productService)
            :base(logger)
        {
            _productService = productService;
        }
        /// <summary>
        /// Product Save enpoint
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("save")]
        [ProducesResponseType(200, Type = typeof(ProductResponseDto))]
        [ProducesResponseType(400, Type = typeof(ProductResponseDto))]
        [ProducesResponseType(500, Type = typeof(ProductResponseDto))]
        [Produces("application/json")]
        public async Task<IActionResult> Save([FromBody] ProductResponseDto request)
        {
            ThrowIfModelStateIsInvalid();
            var result = await _productService.Save(request);
            if (result == null)
            {
                return BadRequest();
            }
            return Ok(result);
        }
        /// <summary>
        /// Get Product endpoint
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("getproduct/{id}")]
        [ProducesResponseType(200, Type = typeof(ProductResponseDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [Produces("application/json")]
        public async Task<IActionResult> GetProduct(Guid? id)
        {
            ThrowIfModelStateIsInvalid();
            var result = await _productService.GetProduct(id);
            if (result == null)
            {
                return BadRequest();
            }
            return Ok(result);
        }
        /// <summary>
        /// Get all product endpoint
        /// </summary>
        /// <returns></returns>
        [HttpGet("getallproducts")]
        [ProducesResponseType(200, Type = typeof(ICollection<ProductResponseDto>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [Produces("application/json")]
        public async Task<IActionResult> GetAllProduct()
        {
            var result = await _productService.GetAllProducts();
            if (result == null)
            {
                return BadRequest();
            }
            return Ok(result);
        }
    }
}
