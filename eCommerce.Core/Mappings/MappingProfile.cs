using AutoMapper;
using eCommerce.Core.Domain;
using eCommerce.Core.DTOs.Category;
using eCommerce.Core.DTOs.CategoryDTO;
using eCommerce.Core.DTOs.Product;
using eCommerce.Core.DTOs.ProductDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Core.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region For Category

            CreateMap<CategoryCreateDto, Category>();
            CreateMap<CategoryUpdateDto, Category>();
            CreateMap<Category, CategoryDto>();

            #endregion

            #region For Product

            CreateMap<ProductCreateDto, Product>();
            CreateMap<ProductUpdateDto, ProductDto>();
            CreateMap<Product, ProductDto>();

            #endregion


        }
    }
}
