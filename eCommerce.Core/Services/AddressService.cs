using AutoMapper;
using eCommerce.Core.Domain;
using eCommerce.Core.DTOs.AddressDTO;
using eCommerce.Core.Interfaces.RepositoryContracts;
using eCommerce.Core.Interfaces.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Core.Services
{
    public class AddressService : IAddressService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AddressService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }
        public async Task<AddressDto> CreateAddress(AddressCreateDto addressCreateDto, Guid customerId)
        {
            var address = new Address()
            {
                CustomerId = customerId,
                AddressLine1 = addressCreateDto.AddressLine1,
                AddressLine2 = addressCreateDto.AddressLine2,
                City = addressCreateDto.City,
                Country = addressCreateDto.Country,
                FullName = addressCreateDto.FullName,
                Landmark = addressCreateDto.Landmark,
                PinCode = addressCreateDto.PinCode,
                PhoneNumber = addressCreateDto.PhoneNumber,
                State = addressCreateDto.State
            };
            await _unitOfWork.Addresses.AddAsync(address);

            var result = await _unitOfWork.SaveChangesAsync();
            if (result > 0)
            {
                return _mapper.Map<AddressDto>(address);
            }
            return null;
        }

        public async Task<bool> DeleteAddress(Guid customerId, Guid addressId)
        {
            var address = await _unitOfWork.Addresses.GetByIdAsync(addressId);

            if (address == null || address.CustomerId != customerId)
            {
                return false;
            }

            _unitOfWork.Addresses.DeleteAsync(address);
            var result = await _unitOfWork.SaveChangesAsync();

            if (result > 0)
            {
                return true;
            }

            return false;
        }

        public async Task<AddressDto> GetAddressById(Guid customerId, Guid addressId)
        {
            var address = await _unitOfWork.Addresses.GetByIdAsync(addressId);

            if (address == null || address.CustomerId != customerId)
            {
                return null;
            }
            return _mapper.Map<AddressDto>(address);
        }

        public async Task<List<AddressDto>> GetAllAddresses(Guid customerId)
        {
            var addressList = await _unitOfWork.Addresses.GetCustomerSpecificAddressByCustomerId(customerId);
            return _mapper.Map<List<AddressDto>>(addressList);
        }

        public async Task<AddressDto> UpdateAddress(AddressUpdateDto addressUpdateDto, Guid customerId)
        {
            var address = await _unitOfWork.Addresses.GetByIdAsync(addressUpdateDto.Id);

            if (address == null || address.CustomerId != customerId)
            {
                return null;
            }

            _mapper.Map(addressUpdateDto, address);
            address.UpdatedDate = DateTime.UtcNow;

            _unitOfWork.Addresses.UpdateAsync(address);
            var result = await _unitOfWork.SaveChangesAsync();

            if (result > 0)
            {
                return _mapper.Map<AddressDto>(address);
            }

            return null;

        }
    }
}
