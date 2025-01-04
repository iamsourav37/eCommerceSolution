using eCommerce.Core.DTOs.ProductDTO;
using eCommerce.Core.Interfaces.ServiceContracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            this._productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var productDtoList = await _productService.GetAllProducts();
            return Ok(productDtoList);
        }


        [HttpGet("productId:Guid")]
        public async Task<IActionResult> Get(Guid productId)
        {
            var productDtoList = await _productService.GetProductById(productId);
            return Ok(productDtoList);
        }

        [HttpPost]
        public async Task<IActionResult> Post(ProductCreateDto productCreateDto)
        {
            var createdProduct = await _productService.CreateProduct(productCreateDto);
            return Ok(createdProduct);

        }

        [HttpPut]
        public async Task<IActionResult> Put(ProductUpdateDto productUpdateDto)
        {
            var updatedProduct = await _productService.UpdateProduct(productUpdateDto);
            return Ok(updatedProduct);
        }

        [HttpDelete("productId: Guid")]
        public async Task<IActionResult> Delete(Guid productid)
        {
            var result = await _productService.DeleteProduct(productid);
            return Ok(result);
        }
    }
}
