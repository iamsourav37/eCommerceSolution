using AutoMapper;
using eCommerce.Core.Domain;
using eCommerce.Core.DTOs.AddressDTO;
using eCommerce.Core.DTOs.Category;
using eCommerce.Core.DTOs.CategoryDTO;
using eCommerce.Core.DTOs.CustomerDTO;
using eCommerce.Core.DTOs.LineItemDTO;
using eCommerce.Core.DTOs.OrderDTO;
using eCommerce.Core.DTOs.Product;
using eCommerce.Core.DTOs.ProductDTO;
using eCommerce.Core.DTOs.WishlistDTO;


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

            #region For Address
            CreateMap<AddressCreateDto, Address>();
            CreateMap<Address, AddressDto>();
            CreateMap<AddressUpdateDto, Address>();
            #endregion

            #region For Customer
            CreateMap<CustomerCreateDto, Customer>();
            CreateMap<Customer, CustomerDto>().ForMember(dest => dest.Orders, opt => opt.Ignore())
                .ForMember(dest => dest.Addresses, opt => opt.MapFrom(src => src.Addresses)); ;
            CreateMap<CustomerUpdateDto, Customer>()
                .ForMember(dest => dest.Addresses, opt => opt.MapFrom(src => src.Addresses));
            #endregion

            #region For Order
            CreateMap<OrderDto, Order>().ReverseMap();

            #endregion

            #region LineItem
            CreateMap<LineItem, LineItemDto>();
            #endregion

            #region Wishlist
            CreateMap<WishlistCreateDto, Wishlist>();
            CreateMap<WishlistDto, Wishlist>().ReverseMap();
            #endregion
        }
    }
}
