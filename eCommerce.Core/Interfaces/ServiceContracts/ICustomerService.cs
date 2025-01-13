using eCommerce.Core.Domain;
using eCommerce.Core.DTOs.CustomerDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Core.Interfaces.ServiceContracts
{
    public interface ICustomerService
    {
        Task<CustomerDto> CreateCustomerAsync(CustomerCreateDto customerCreateDto);
        Task<List<CustomerDto>> GetAllCustomerAsync();
        Task<CustomerDto> GetCustomerByIdAsync(Guid customerId);
        Task<CustomerDto> UpdateCustomerAsync(CustomerUpdateDto customerUpdateDto);
        Task<Guid> GetCustomerIdByAccountId(Guid accountId);

    }
}
