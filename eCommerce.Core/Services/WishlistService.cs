using AutoMapper;
using eCommerce.Core.Domain;
using eCommerce.Core.DTOs.WishlistDTO;
using eCommerce.Core.Interfaces.RepositoryContracts;
using eCommerce.Core.Interfaces.ServiceContracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Core.Services
{
    public class WishlistService : IWishlistService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public WishlistService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }
        public async Task<WishlistDto> CreateWishlist(WishlistCreateDto wishlistCreateDto)
        {
            var wishlist = _mapper.Map<Wishlist>(wishlistCreateDto);
            await _unitOfWork.Wishlist.AddAsync(wishlist);
            var result = await _unitOfWork.SaveChangesAsync();

            if (result > 0)
            {
                // Reload the wishlist with the Product entity included
                wishlist = await _unitOfWork.Wishlist.GetWishlistByCustomerIdAndProductId(wishlistCreateDto.CustomerId, wishlistCreateDto.ProductId);


                return new WishlistDto()
                {
                    CustomerId = wishlist.CustomerId,
                    ProductId = wishlist.ProductId,
                    CreatedDate = wishlist.CreatedDate,
                    ProductName = wishlist.Product.Name,
                    ProductPrice = wishlist.Product.Price,
                    UpdatedDate = wishlist.UpdatedDate
                };
            }
            return null;
        }

        public async Task<List<WishlistDto>> GetAllWishlist(Guid customerId)
        {

            var wishlistList = await _unitOfWork.Wishlist.GetWishlistByCustomerId(customerId);

            return _mapper.Map<List<WishlistDto>>(wishlistList);
        }

        public async Task<bool> RemoveWishlist(WishlistRemoveDto wishlistRemoveDto)
        {
            // Find the wishlist item
            var wishlistItem = await _unitOfWork.Wishlist.GetWishlistByCustomerIdAndProductId(wishlistRemoveDto.CustomerId, wishlistRemoveDto.ProductId);

            if (wishlistItem == null)
            {
                return false;
            }

            _unitOfWork.Wishlist.DeleteAsync(wishlistItem);
            var result = await _unitOfWork.SaveChangesAsync();

            if (result == 0)
            {
                return false;
            }

            return true;

        }
    }
}
