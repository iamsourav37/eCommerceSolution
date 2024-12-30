
namespace eCommerce.Core.Interfaces.RepositoryContracts
{
    public interface IUnitOfWork : IDisposable
    {
        IOrderRepository Orders { get; }

        Task<int> SaveChangesAsync();
    }
}
