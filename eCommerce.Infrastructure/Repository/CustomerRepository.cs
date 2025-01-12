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
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        public CustomerRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }

        public async Task<Customer> GetCustomerIdByAccountId(Guid accountId)
        {
            // Retrieve the Customer ID where the AccountId matches
            var customer = await _context.Customers
                .FirstOrDefaultAsync(c => c.AccountId == accountId);

            // If no customer is found, you might want to throw an exception or return a default value
            if (customer == null)
            {
                throw new KeyNotFoundException("Customer not found for the given account ID.");
            }

            return customer; // Return the Customer ID
        }
    }
}
