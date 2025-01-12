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

        public Task<bool> DeleteCategoryById(Guid categoryId)
        {
            throw new NotImplementedException();
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
            var categoryDomainModel = _mapper.Map<Category>(categoryUpdateDto);
            categoryDomainModel.UpdatedDate = DateTime.UtcNow;
            _unitOfWork.Categories.UpdateAsync(categoryDomainModel);
            int result = await _unitOfWork.SaveChangesAsync();

            if (result > 0)
            {
                var categoryDto = _mapper.Map<CategoryDto>(categoryDomainModel);
                return categoryDto;
            }

            return null;

        }
    }
}
