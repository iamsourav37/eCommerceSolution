using AutoMapper;
using eCommerce.Core.Domain;
using eCommerce.Core.DTOs.Category;
using eCommerce.Core.DTOs.CustomerDTO;
using eCommerce.Core.Interfaces.RepositoryContracts;
using eCommerce.Core.Interfaces.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Core.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CustomerService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }

        public async Task<CustomerDto> CreateCustomerAsync(CustomerCreateDto customerCreateDto)
        {
            var customerDomainModel = _mapper.Map<Customer>(customerCreateDto);
            await _unitOfWork.Customers.AddAsync(customerDomainModel);
            var result = await _unitOfWork.SaveChangesAsync();
            if (result > 0)
            {
                var customerDto = _mapper.Map<CustomerDto>(customerDomainModel);
                return customerDto;
            }
            return null;
        }

        public async Task<List<CustomerDto>> GetAllCustomerAsync()
        {
            var allCustomer = await _unitOfWork.Customers.GetAllAsync(include:"Addresses");
            var customerDtoList = _mapper.Map<List<CustomerDto>>(allCustomer);
            return customerDtoList;
        }

        public async Task<CustomerDto> GetCustomerByIdAsync(Guid customerId)
        {
            var customer = await _unitOfWork.Customers.GetByIdAsync(customerId, include: "Addresses");
            var customerDto = _mapper.Map<CustomerDto>(customer);
            return customerDto;
        }

        public async Task<CustomerDto> UpdateCustomerAsync(CustomerUpdateDto customerUpdateDto)
        {
            var customer = await _unitOfWork.Customers.GetCustomerIdByAccountId(customerUpdateDto.AccountId);
            customerUpdateDto.Id = customer.Id;
            _mapper.Map(customerUpdateDto, customer);
            customer.UpdatedDate = DateTime.Now;
            _unitOfWork.Customers.UpdateAsync(customer);
            var result = await _unitOfWork.SaveChangesAsync();

            if (result > 0)
            {
                var customerDto = _mapper.Map<CustomerDto>(customer);
                return customerDto;
            }

            return null;
        }
    }
}
