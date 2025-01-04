using eCommerce.API.Utility;
using eCommerce.Core.DTOs.ProductDTO;
using eCommerce.Core.Interfaces.ServiceContracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.Json;

namespace eCommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private ApiResponse _apiResponse;

        public ProductController(IProductService productService)
        {
            this._productService = productService;
            _apiResponse = new ApiResponse();
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var productDtoList = await _productService.GetAllProducts();
            _apiResponse.SetResponse(true, 200, productDtoList, null);
            return Ok(_apiResponse);
        }


        [HttpGet("productId:Guid")]
        public async Task<IActionResult> Get(Guid productId)
        {
            var productDtoList = await _productService.GetProductById(productId);
            _apiResponse.SetResponse(true, 200, productDtoList, null);
            return Ok(_apiResponse);
        }

        [HttpPost]
        public async Task<IActionResult> Post(ProductCreateDto productCreateDto)
        {
            var createdProduct = await _productService.CreateProduct(productCreateDto);
            _apiResponse.SetResponse(true, 201, createdProduct, null);
            return Ok(createdProduct);

        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] ProductUpdateDto productUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                _apiResponse.SetResponse(false, 400, null, ["Invalid Data"]);
                return BadRequest(_apiResponse);
            }
            var updatedProduct = await _productService.UpdateProduct(productUpdateDto);
            _apiResponse.SetResponse(true, 200, updatedProduct, null);
            return Ok(updatedProduct);
        }

        [HttpDelete("productId: Guid")]
        public async Task<IActionResult> Delete(Guid productid)
        {
            var result = await _productService.DeleteProduct(productid);
            _apiResponse.SetResponse(true, 200, "Successfully Deleted.", null);
            return Ok(_apiResponse);
        }
    }
}
