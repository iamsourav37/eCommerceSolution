using eCommerce.Core.DTOs.AddressDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Core.Interfaces.ServiceContracts
{
    public interface IAddressService
    {
        Task<AddressDto> CreateAddress(AddressCreateDto addressCreateDto, Guid customerId);
        Task<AddressDto> UpdateAddress(AddressUpdateDto addressUpdateDto, Guid customerId);
        Task<bool> DeleteAddress(Guid customerId, Guid addressId);
        Task<List<AddressDto>> GetAllAddresses(Guid customerId);
        Task<AddressDto> GetAddressById(Guid customerId, Guid addressId);
    }
}
