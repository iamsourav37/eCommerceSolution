using AutoMapper;
using eCommerce.Core.Domain;
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
    }
}
