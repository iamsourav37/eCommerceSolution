using eCommerce.Core.Domain;
using eCommerce.Core.Interfaces.RepositoryContracts;
using eCommerce.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Infrastructure.Repository
{
    public class WishlistRepository : Repository<Wishlist>, IWishlistRepository
    {
        public WishlistRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }

        public async Task<List<Wishlist>> GetWishlistByCustomerId(Guid customerId)
        {
           var wishlists = await _context.Wishlists.Where(wishlist => wishlist.CustomerId == customerId).Include(w => w.Product).ToListAsync();
            return wishlists;
        }

        public async Task<Wishlist?> GetWishlistByCustomerIdAndProductId(Guid customerId, Guid productId)
        {
            return await _context.Wishlists.Include(w => w.Product).FirstOrDefaultAsync(wishlist => wishlist.CustomerId == customerId && wishlist.ProductId == productId);
        }
    }
}
