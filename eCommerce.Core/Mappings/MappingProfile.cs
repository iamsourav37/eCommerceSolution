using AutoMapper;
using eCommerce.Core.Domain;
using eCommerce.Core.DTOs.Category;
using eCommerce.Core.DTOs.CategoryDTO;
using eCommerce.Core.DTOs.CustomerDTO;
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
            CreateMap<Category, CategoryResponse>();

            #endregion

            #region For Product

            CreateMap<ProductCreateDto, Product>().ForMember(dest => dest.Categories, opt => opt.Ignore());
            CreateMap<ProductUpdateDto, Product>();
            CreateMap<Product, ProductDto>()
           .ForMember(dest => dest.Categories,
                      opt => opt.MapFrom(src => src.Categories));

            #endregion


            #region For Customer
            CreateMap<CustomerCreateDto, Customer>();
            CreateMap<Customer, CustomerDto>().ForMember(dest => dest.Orders, opt => opt.Ignore());
            #endregion


        }
    }
}
