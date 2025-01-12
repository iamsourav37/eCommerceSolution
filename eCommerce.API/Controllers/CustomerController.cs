using eCommerce.API.Utility;
using eCommerce.API.ViewModels;
using eCommerce.Core.Domain;
using eCommerce.Core.DTOs.CustomerDTO;
using eCommerce.Core.Interfaces.ServiceContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace eCommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ApiResponse _response;
        public CustomerController(ICustomerService customerService, IHttpContextAccessor httpContextAccessor)
        {
            this._customerService = customerService;
            this._httpContextAccessor = httpContextAccessor;
            _response = new ApiResponse();
        }

        [Authorize(Roles = Constants.ADMIN_ROLE)]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var allCustomer = await _customerService.GetAllCustomerAsync();
            _response.SetResponse(true, 200, allCustomer, null);
            return Ok(_response);
        }


        [Authorize]
        [HttpGet("{customerId:guid}")]
        public async Task<IActionResult> Get(Guid customerId)
        {
            var customerDto = await _customerService.GetCustomerByIdAsync(customerId);
            _response.SetResponse(true, 200, customerDto, null);
            return Ok(_response);
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> Put(CustomerUpdateViewModel customerUpdateViewModel)
        {
            var currentAccountid = GetCurrentUserId();
            var customerUpdateDto = new CustomerUpdateDto()
            {
                AccountId = currentAccountid,
                Addresses = customerUpdateViewModel.Addresses,
                Name = customerUpdateViewModel.Name,
            };
            var updatedCustomer = await _customerService.UpdateCustomerAsync(customerUpdateDto);

            if (updatedCustomer != null)
            {
                _response.SetResponse(true, 200, updatedCustomer, null);
                return Ok(_response);
            }
            _response.SetResponse(false, 400, null, ["Failed to update customer"]);
            return BadRequest(_response);
        }

        private Guid GetCurrentUserId()
        {
            var userIdClaim = _httpContextAccessor.HttpContext?.User.Claims
                .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

            return userIdClaim != null ? Guid.Parse(userIdClaim.Value) : Guid.Empty;
        }


    }
}
