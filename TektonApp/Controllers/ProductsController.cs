using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TektonApp.Application.Common.Interfaces.Services;
using TektonApp.Application.Dtos.Request;

namespace TektonApp.Presentation.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("/api/products")]
    [AllowAnonymous]
    public class ProductsController : ControllerBase
    {
        #region Constructor
        private readonly IProductService _productService;
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }
        #endregion

        #region public async Task<IActionResult> GetProductById(int id)
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            return Ok(await _productService.GetProductByIdAsync(id));
        }
        #endregion

        #region public async Task<IActionResult> GetProducts()
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetProducts()
        {
            var _Products = await _productService.GetProductsAsync();
            return Ok(_Products);
        }
        #endregion

        #region public async Task<IActionResult> CreateProduct([FromBody] ProductRequestDto Product)
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] ProductRequestDto Product)
        {
            var _result = await _productService.CreateProductAsync(Product);
            switch (_result.StatusCode)
            {
                case 200:
                    return CreatedAtAction(nameof(GetProductById), new { id = _result.Result }, _result.Result);

                case 400:
                    return BadRequest(_result.Result);

                default:
                    return BadRequest(_result.Result);
            }
        }
        #endregion

        #region public async Task<IActionResult> UpdateProduct([FromBody] ProductRequestDto Product)
        [HttpPut]
        public async Task<IActionResult> UpdateProduct([FromBody] ProductRequestDto Product)
        {
            var _result = await _productService.UpdateProductAsync(Product);
            switch (_result.StatusCode)
            {
                case 200:
                    return Ok(_result.Result);

                case 400:
                    return BadRequest(_result.Result);

                default:
                    return BadRequest(_result.Result);
            }
        }
        #endregion

        #region public async Task<IActionResult> DeleteProduct(int id)
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var _result = await _productService.DeleteProductAsync(id);
            switch (_result.StatusCode)
            {
                case 200:
                    return Ok(_result.Result);

                case 400:
                    return BadRequest(_result.Result);

                default:
                    return BadRequest(_result.Result);
            }
        }
        #endregion
    }
}
