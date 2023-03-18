using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace FunCart.Controllers
{
    [Authorize]
    [Route("api/v1")]
    [ApiController]
    public class ProductController : BaseController
    {
        private readonly IProductService _productService;
        private readonly IHttpContextAccessor _contextAccessor;
        public ProductController(IHttpContextAccessor ca, IProductService productService) : base(ca)
        {
            _productService=productService; 
            _contextAccessor=ca;
        }
        /// <summary>
        /// Register Product Controller
        /// </summary>
        /// <param name="productRegister"></param>
        /// <returns></returns>
        [HttpPost("Product")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Register([FromBody] ProductRegister productRegister)
        {
            var response=await _productService.ProductRegister(productRegister, UserId);
            if (response.IsSuccess)
                return Ok(response);
            return BadRequest(response);

        }
        /// <summary>
        /// GetProduct
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("Product/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetProduct([Required]Guid id)
        {
           var response=await _productService.GetProduct(id);
            if(response.HasError)
                return NotFound(response);
            if (response.Exception != null)
                return BadRequest(response);

            return Ok(response);

        }
        /// <summary>
        /// Delete Product Controller
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("Product/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> DeleteProduct([Required] Guid id)
        {
            var response = await _productService.DeleteProduct(id, UserId);
            if (response.IsSuccess)
                return Ok(response);
            return BadRequest(response);

        }
        /// <summary>
        /// GetAllProduct
        /// </summary>
        ///  /// <param name="pageNumber"></param>
        ///  /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet("Product/GetAllProducts")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetAllProducts([Required] int pageNumber, [Required] int pageSize)
        {
            var response = await _productService.GetAllProducts(pageNumber, pageSize);
            if (response.IsSuccess)
                return Ok(response);
            return BadRequest(response);

        }
        /// <summary>
        /// UpdateProduct
        /// </summary>
        /// <param name="updateProduct"></param>
        /// <returns></returns>
        [HttpPut("Product")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> UpdateProduct([FromBody] UpdateProduct updateProduct)
        {
            var response = await _productService.UpdateProduct(updateProduct, UserId);
            if (response.HasError)
                return NotFound(response);
            if (response.Exception != null)
                return BadRequest(response);

            return Ok(response);

        }

    }
}
