﻿using eCommerce.Core.Domain;
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
    internal class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Order>> GetOrderByCustomerIdAsync(Guid customerId)
        {
            return await this._dbSet.Where(order => order.CustomerId == customerId).Include(o => o.Customer)
                                    .Include(o => o.LineItems).ThenInclude(li => li.Product)
                                    .Include(o => o.ShippingAddress)
                                    .ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetOrdersWithDetailsAsync()
        {
            return await this._dbSet.Include(o => o.LineItems)
                                    .Include(o => o.ShippingAddress).ToListAsync();
        }
    }
}
