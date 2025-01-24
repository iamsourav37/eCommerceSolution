using eCommerce.Core.Domain;

namespace eCommerce.Core.Interfaces.RepositoryContracts
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<IEnumerable<Order>> GetOrdersWithDetailsAsync();
        Task<IEnumerable<Order>> GetOrderByCustomerIdAsync(Guid customerId);
    }
}
