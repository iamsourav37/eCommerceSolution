using eCommerce.Core.DTOs.Category;
using eCommerce.Core.DTOs.CategoryDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Core.Interfaces.ServiceContracts
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDto>> GetAllCategories();
        Task<CategoryDto> GetCategoryById(Guid categoryId);
        Task<CategoryDto> CreateCategory(CategoryCreateDto categoryCreateDto);
        Task<CategoryDto> UpdateCategory(CategoryUpdateDto categoryUpdateDto);
        Task<bool> DeleteCategoryById(Guid categoryId);
    }
}
