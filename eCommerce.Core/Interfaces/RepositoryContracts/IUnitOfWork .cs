
namespace eCommerce.Core.Interfaces.RepositoryContracts
{
    public interface IUnitOfWork : IDisposable
    {
        IOrderRepository Orders { get; }
        ICategoryRepository Categories { get; }
        IProductRepository Products { get; }
        ICustomerRepository Customers { get; }
        IAddressRepository Addresses { get; }
        IWishlistRepository Wishlist { get; }

        Task<int> SaveChangesAsync();
    }
}
