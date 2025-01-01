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

        [HttpPost]
        public async Task<IActionResult> Post(ProductCreateDto productCreateDto)
        {
            var createdProduct = await _productService.CreateProduct(productCreateDto);
            return Ok(createdProduct);

        }
    }
}
