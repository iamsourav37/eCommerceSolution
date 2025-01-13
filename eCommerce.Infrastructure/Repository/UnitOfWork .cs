using eCommerce.Core.Interfaces.RepositoryContracts;
using eCommerce.Infrastructure.DbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private IOrderRepository _orderRepository;
        private ICustomerRepository _customerRepository;
        private IProductRepository _productRepository;
        private ICategoryRepository _categoryRepository;
        private IAddressRepository _addressRepository;


        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public IOrderRepository Orders => _orderRepository ?? new OrderRepository(_context);
        public ICustomerRepository Customers => _customerRepository ?? new CustomerRepository(_context);
        public IProductRepository Products => _productRepository ?? new ProductRepository(_context);
        public ICategoryRepository Categories => _categoryRepository ?? new CategoryRepository(_context);
        public IAddressRepository Addresses => _addressRepository ?? new AddressRepository(_context);

        public void Dispose()
        {
            _context.Dispose();
        }

        public Task<int> SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}
