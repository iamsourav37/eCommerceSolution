using eCommerce.Core.Domain;
using eCommerce.Core.DTOs.AddressDTO;
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
    public class AddressRepository : Repository<Address>, IAddressRepository
    {
        public AddressRepository(ApplicationDbContext context) : base(context)
        {

        }

        public async Task<List<Address>> GetCustomerSpecificAddressByCustomerId(Guid customerId)
        {
            return await _context.Addresses.Where(address => address.CustomerId == customerId).ToListAsync();

        }
    }
}
