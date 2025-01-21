using eCommerce.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Core.Interfaces.RepositoryContracts
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<IEnumerable<Order>> GetOrdersWithDetailsAsync();
        Task<IEnumerable<Order>> GetOrderByCustomerIdAsync(Guid customerId);
    }
}
