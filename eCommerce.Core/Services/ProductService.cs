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
            var categoryIdList = productCreateDto.CategoryIds;
            var categoryList = new List<Category>();
            // Fetch categories from the database
            foreach (var categoryId in categoryIdList)
            {
                var category = await _unitOfWork.Categories.GetByIdAsync(categoryId);

                if (category == null)
                {
                    throw new Exception("No valid categories found for the provided Category IDs.");
                }
                categoryList.Add(category);
            }



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
            var product = await _unitOfWork.Products.GetByIdAsync(productId);
            var productDto = _mapper.Map<ProductDto>(product);
            return productDto;
        }

        public async Task<ProductDto> UpdateProduct(ProductUpdateDto productUpdateDto)
        {
            var product = _mapper.Map<Product>(productUpdateDto);
            _unitOfWork.Products.UpdateAsync(product);
            var result = await _unitOfWork.SaveChangesAsync();
            var productDto = _mapper.Map<ProductDto>(product);
            return productDto;
        }
    }
}
