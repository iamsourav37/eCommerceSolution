using eCommerce.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Core.Interfaces.RepositoryContracts
{
    public interface IWishlistRepository : IRepository<Wishlist> 
    {

        Task<List<Wishlist>> GetWishlistByCustomerId(Guid customerId);
        Task<Wishlist?> GetWishlistByCustomerIdAndProductId(Guid customerId, Guid productId); 
    }
}
