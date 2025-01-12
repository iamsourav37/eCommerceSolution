using AutoMapper;
using eCommerce.Core.Domain;
using eCommerce.Core.DTOs.Category;
using eCommerce.Core.DTOs.CategoryDTO;
using eCommerce.Core.Interfaces.RepositoryContracts;
using eCommerce.Core.Interfaces.ServiceContracts;


namespace eCommerce.Core.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CategoryService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this._mapper = mapper;
            this._unitOfWork = unitOfWork;
        }

        public async Task<CategoryDto> CreateCategory(CategoryCreateDto categoryCreateDto)
        {
            var categoryDomainModel = _mapper.Map<Category>(categoryCreateDto);
            await _unitOfWork.Categories.AddAsync(categoryDomainModel);
            int result = await _unitOfWork.SaveChangesAsync();
            if (result == 0)
            {
                throw new InvalidOperationException("Failed to save the category.");
            }

            var categoryDto = _mapper.Map<CategoryDto>(categoryDomainModel);
            return categoryDto;
        }

        public async Task<bool> DeleteCategoryById(Guid categoryId)
        {
            // Retrieve the existing category from the database
            var existingCategory = await _unitOfWork.Categories.GetByIdAsync(categoryId);

            if (existingCategory == null)
            {
                // Optionally, throw an exception or return null/error if not found
                return false;
            }
            _unitOfWork.Categories.DeleteAsync(existingCategory);
            var result = await _unitOfWork.SaveChangesAsync();

            if (result == 0)
            {
                return false;
            }

            return true;
        }

        public async Task<IEnumerable<CategoryDto>> GetAllCategories()
        {
            var categoryList = await _unitOfWork.Categories.GetAllAsync("");
            var categoryDtoList = _mapper.Map<List<CategoryDto>>(categoryList);
            return categoryDtoList;
        }

        public async Task<CategoryDto> GetCategoryById(Guid categoryId)
        {
            var category = await _unitOfWork.Categories.GetByIdAsync(categoryId);
            if (category == null)
            {
                return null;
            }
            var categoryDto = _mapper.Map<CategoryDto>(category);
            return categoryDto;
        }

        public async Task<CategoryDto> UpdateCategory(CategoryUpdateDto categoryUpdateDto)
        {
            // Retrieve the existing category from the database
            var existingCategory = await _unitOfWork.Categories.GetByIdAsync(categoryUpdateDto.Id);

            if (existingCategory == null)
            {
                // Optionally, throw an exception or return null/error if not found
                return null;
            }

            // Map only updated fields from the DTO to the existing entity
            _mapper.Map(categoryUpdateDto, existingCategory);

            // Update audit fields
            existingCategory.UpdatedDate = DateTime.UtcNow;

            // Update the category in the database
            _unitOfWork.Categories.UpdateAsync(existingCategory);
            int result = await _unitOfWork.SaveChangesAsync();

            if (result > 0)
            {
                // Map the updated entity to a DTO and return
                return _mapper.Map<CategoryDto>(existingCategory);
            }

            // Return null if no changes were saved
            return null;
        }

    }
}
