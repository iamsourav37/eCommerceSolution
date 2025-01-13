using eCommerce.API.Helper;
using eCommerce.API.Utility;
using eCommerce.Core.DTOs.AddressDTO;
using eCommerce.Core.Interfaces.ServiceContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddressService _addressService;
        private readonly ICustomerService _customerService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ApiResponse _response;
        private Guid? _currentUserId;
        private Guid? _currentUserCustomerId;

        public AddressController(IAddressService addressService, ICustomerService customerService, IHttpContextAccessor httpContextAccessor)
        {
            this._addressService = addressService;
            this._customerService = customerService;
            this._httpContextAccessor = httpContextAccessor;
            _response = new ApiResponse();
        }
        private Guid CurrentUserId
        {
            get
            {
                if (!_currentUserId.HasValue)
                {
                    _currentUserId = UserHelper.GetCurrentUserId(_httpContextAccessor);
                }
                return _currentUserId.Value;
            }
        }

        private async Task<Guid> GetCurrentUserCustomerIdAsync()
        {
            if (!_currentUserCustomerId.HasValue)
            {
                _currentUserCustomerId = (await _customerService.GetCustomerIdByAccountId(CurrentUserId)).Id;
            }
            return _currentUserCustomerId.Value;
        }


        [HttpPost("{customerId:guid}")]
        public async Task<IActionResult> Post([FromBody] AddressCreateDto addressCreateDto, [FromRoute] Guid customerId)
        {
            // Fetch current user's customer ID
            var currentUserCustomerId = await GetCurrentUserCustomerIdAsync();


            if (_currentUserCustomerId != customerId)
            {
                _response.SetResponse(false, 400, null, ["Customer Id is not matched."]);
                return BadRequest(_response);
            }

            var createdAddress = await _addressService.CreateAddress(addressCreateDto, customerId);
            _response.SetResponse(true, 201, createdAddress, null);
            return Ok(_response);
        }

        [HttpGet("{customerId:guid}")]
        public async Task<IActionResult> Get(Guid customerId)
        {
            // Fetch current user's customer ID
            var currentUserCustomerId = await GetCurrentUserCustomerIdAsync();


            if (_currentUserCustomerId != customerId)
            {
                _response.SetResponse(false, 400, null, ["Customer Id is not matched."]);
                return BadRequest(_response);
            }

            var allAddressesList = await _addressService.GetAllAddresses(customerId);
            _response.SetResponse(true, 200, allAddressesList, null);
            return Ok(_response);
        }

        [HttpGet("{customerId:guid}/{addressId:guid}")]
        public async Task<IActionResult> Get(Guid customerId, Guid addressId)
        {
            // Fetch current user's customer ID
            var currentUserCustomerId = await GetCurrentUserCustomerIdAsync();

            if (_currentUserCustomerId != customerId)
            {
                _response.SetResponse(false, 400, null, ["Customer Id is not matched."]);
                return BadRequest(_response);
            }


            var addressDto = await _addressService.GetAddressById(customerId, addressId);
            _response.SetResponse(true, 200, addressDto, null);
            return Ok(_response);
        }

        [HttpPut("{customerId:guid}")]
        public async Task<IActionResult> Put([FromBody] AddressUpdateDto addressUpdateDto, [FromRoute] Guid customerId)
        {
            // Fetch current user's customer ID
            var currentUserCustomerId = await GetCurrentUserCustomerIdAsync();

            if (_currentUserCustomerId != customerId)
            {
                _response.SetResponse(false, 400, null, ["Customer Id is not matched."]);
                return BadRequest(_response);
            }

            var updatedAddress = await _addressService.UpdateAddress(addressUpdateDto, customerId);
            _response.SetResponse(true, 200, updatedAddress, null);
            return Ok(_response);
        }

        [HttpDelete("{customerId:guid}/{addressId:guid}")]
        public async Task<IActionResult> Delete(Guid customerId, Guid addressId)
        {
            var isDeleted = await _addressService.DeleteAddress(customerId, addressId);

            if(isDeleted)
            {
                _response.SetResponse(true, 200, "Address is deleted!", null);
                return Ok(_response);
            }

            _response.SetResponse(false, 404, null, ["Not found !"]);
            return NotFound(_response);
        }
    }
}
