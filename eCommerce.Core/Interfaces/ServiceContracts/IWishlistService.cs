using eCommerce.Core.DTOs.WishlistDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Core.Interfaces.ServiceContracts
{
    public interface IWishlistService
    {
        Task<WishlistDto> CreateWishlist(WishlistCreateDto wishlistCreateDto);
        Task<bool> RemoveWishlist(WishlistRemoveDto wishlistRemoveDto);
        Task<List<WishlistDto>> GetAllWishlist(Guid customerId);
    }
}
