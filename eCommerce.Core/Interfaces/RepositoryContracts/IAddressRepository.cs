using eCommerce.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Core.Interfaces.RepositoryContracts
{
    public interface IAddressRepository : IRepository<Address>
    {
        Task<List<Address>> GetCustomerSpecificAddressByCustomerId(Guid customerId);
    }
}
