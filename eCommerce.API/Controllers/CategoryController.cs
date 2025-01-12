using eCommerce.API.Utility;
using eCommerce.Core.DTOs.Category;
using eCommerce.Core.DTOs.CategoryDTO;
using eCommerce.Core.Interfaces.ServiceContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata;

namespace eCommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private ApiResponse _response;

        public CategoryController(ICategoryService categoryService)
        {
            this._categoryService = categoryService;
            _response = new ApiResponse();
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var allCategory = await _categoryService.GetAllCategories();
            _response.SetResponse(true, 200, allCategory, null);
            return Ok(_response);
        }

        [HttpGet("{categoryId:guid}")]
        public async Task<IActionResult> Get(Guid categoryId)
        {
            var categoryDto = await _categoryService.GetCategoryById(categoryId);

            if (categoryDto == null)
            {
                _response.SetResponse(false, 404, null, [$"{categoryId} not found !"]);
                return NotFound(_response);
            }
            _response.SetResponse(true, 200, categoryDto, null);
            return Ok(_response);
        }


        [Authorize(Roles = Constants.ADMIN_ROLE)]
        [HttpPost]
        public async Task<IActionResult> Post(CategoryCreateDto categoryCreateDto)
        {
            var createdCategory = await _categoryService.CreateCategory(categoryCreateDto);
            if (createdCategory == null)
            {
                _response.SetResponse(false, 400, null, ["Failed to create category !"]);
                return BadRequest(_response);
            }
            _response.SetResponse(true, 201, createdCategory, null);
            return Ok(_response);
        }

        [Authorize(Roles = Constants.ADMIN_ROLE)]
        [HttpPut]
        public async Task<IActionResult> Put(CategoryUpdateDto categoryUpdateDto)
        {
            var updatedCategory = await _categoryService.UpdateCategory(categoryUpdateDto);

            if (updatedCategory == null)
            {
                _response.SetResponse(false, 400, null, ["Failed to update category !"]);
                return BadRequest(_response);
            }
            _response.SetResponse(true, 200, updatedCategory, null);
            return Ok(_response);
        }

        [HttpDelete("{categoryId:guid}")]
        public async Task<IActionResult> Delete(Guid categoryId)
        {

            var isDeleted = await _categoryService.DeleteCategoryById(categoryId);

            if(isDeleted)
            {
                _response.SetResponse(true, 200, "Deleted Successfully !", null);
                return Ok(_response);
            }

            _response.SetResponse(false, 404, null, ["Failed to delete the category !"]);
            return NotFound(_response);

        }

    }
}
