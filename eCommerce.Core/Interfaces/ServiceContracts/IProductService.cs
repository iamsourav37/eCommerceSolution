using eCommerce.Core.DTOs.Product;
using eCommerce.Core.DTOs.ProductDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Core.Interfaces.ServiceContracts
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAllProducts();
        Task<ProductDto> GetProductById(Guid productId);
        Task<ProductDto> CreateProduct(ProductCreateDto productCreateDto);
        Task<ProductDto> UpdateProduct(ProductUpdateDto productUpdateDto);
        Task<bool> DeleteProduct(Guid productId);
    }
}
