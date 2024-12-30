using eCommerce.Core.DTOs.CategoryDTO;
using eCommerce.Core.Interfaces.ServiceContracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublicController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public PublicController(ICategoryService categoryService)
        {
            this._categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var allCategory = await _categoryService.GetAllCategories();
            return Ok(allCategory);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(CategoryCreateDto categoryCreateDto)
        {
            var createdCategory = await _categoryService.CreateCategory(categoryCreateDto);
            return Ok(createdCategory);
        }
    }
}
