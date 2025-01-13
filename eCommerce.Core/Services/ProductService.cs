using AutoMapper;
using eCommerce.Core.Domain;
using eCommerce.Core.DTOs.Product;
using eCommerce.Core.DTOs.ProductDTO;
using eCommerce.Core.Interfaces.RepositoryContracts;
using eCommerce.Core.Interfaces.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Core.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }
        public async Task<ProductDto> CreateProduct(ProductCreateDto productCreateDto)
        {
            var categoryList = await this.GetCategoryList(productCreateDto.CategoryIds);

            var productDomain = _mapper.Map<Product>(productCreateDto);
            productDomain.Categories = categoryList;
            await _unitOfWork.Products.AddAsync(productDomain);
            var result = await _unitOfWork.SaveChangesAsync();

            var productDto = _mapper.Map<ProductDto>(productDomain);
            return productDto;
        }

        public async Task<bool> DeleteProduct(Guid productId)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(productId);

            if (product == null)
            {
                return false;
            }
            _unitOfWork.Products.DeleteAsync(product);
            var result = await _unitOfWork.SaveChangesAsync();
            return result > 0;
        }

        public async Task<IEnumerable<ProductDto>> GetAllProducts()
        {
            var products = await _unitOfWork.Products.GetAllAsync(include: "Categories");
            var productDtoList = _mapper.Map<List<ProductDto>>(products);

            return productDtoList;
        }

        public async Task<ProductDto> GetProductById(Guid productId)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(productId, "Categories");
            var productDto = _mapper.Map<ProductDto>(product);
            return productDto;
        }

        public async Task<ProductDto> UpdateProduct(ProductUpdateDto productUpdateDto)
        {
            var existingProduct = await _unitOfWork.Products.GetByIdAsync(productUpdateDto.Id, "Categories");

            if (existingProduct == null)
            {
                throw new InvalidOperationException("Product not found. Invalid Id");
            }

            _mapper.Map(productUpdateDto, existingProduct);
            var categoryList = await this.GetCategoryList(productUpdateDto.CategoryIds);

            // Clearing the Categories first for existing category duplicate scenario
            existingProduct.Categories?.Clear();
            await _unitOfWork.SaveChangesAsync();


            existingProduct.Categories = categoryList;
            existingProduct.UpdatedDate = DateTime.UtcNow;

            _unitOfWork.Products.UpdateAsync(existingProduct);
            var result = await _unitOfWork.SaveChangesAsync();
            var productDto = _mapper.Map<ProductDto>(existingProduct);
            return productDto;
        }


        private async Task<List<Category>> GetCategoryList(ICollection<Guid> categoryIds)
        {
            var categoryList = new List<Category>();
            // Fetch categories from the database
            foreach (var categoryId in categoryIds)
            {
                var category = await _unitOfWork.Categories.GetByIdAsync(categoryId);

                if (category == null)
                {
                    throw new Exception("No valid categories found for the provided Category IDs.");
                }
                categoryList.Add(category);
            }
            return categoryList;
        }
    }
}
