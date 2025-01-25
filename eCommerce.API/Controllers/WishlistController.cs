using eCommerce.API.Helper;
using eCommerce.API.Utility;
using eCommerce.Core.DTOs.WishlistDTO;
using eCommerce.Core.Interfaces.ServiceContracts;
using eCommerce.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class WishlistController : ControllerBase
    {
        private readonly IWishlistService _wishlistService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICustomerService _customerService;
        private Guid _currentUserCustomerId;
        private ApiResponse _apiResponse;

        public WishlistController(IWishlistService wishlistService, IHttpContextAccessor httpContextAccessor, ICustomerService customerService)
        {
            this._wishlistService = wishlistService;
            this._httpContextAccessor = httpContextAccessor;
            this._customerService = customerService;
            _apiResponse = new ApiResponse();
        }

        private async Task<Guid> GetCurrentUserCustomerIdAsync()
        {
            var currentAccountId = UserHelper.GetCurrentUserId(_httpContextAccessor);
            _currentUserCustomerId = (await _customerService.GetCustomerIdByAccountId(currentAccountId)).Id;
            return _currentUserCustomerId;
        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            await GetCurrentUserCustomerIdAsync();
            var wishlistDtos = await _wishlistService.GetAllWishlist(_currentUserCustomerId);
            _apiResponse.SetResponse(true, 200, wishlistDtos, null);
            return Ok(_apiResponse);
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody] WishlistCreateDto wishlistCreateDto)
        {
            var createdWishlist = await _wishlistService.CreateWishlist(wishlistCreateDto);

            if (createdWishlist == null)
            {
                _apiResponse.SetResponse(false, 400, null, ["Failed to create the Wishlist"]);
                return BadRequest(_apiResponse);
            }

            _apiResponse.SetResponse(true, 201, createdWishlist, null);
            return Ok(_apiResponse);

        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] WishlistRemoveDto wishlistRemoveDto)
        {
            var isDeleted = await _wishlistService.RemoveWishlist(wishlistRemoveDto);

            if(!isDeleted)
            {
                _apiResponse.SetResponse(false, 400, null, ["Remove Wishlist Failed."]);
                return BadRequest(_apiResponse);
            }

            _apiResponse.SetResponse(true, 200, "Removed Successfully.", null);
            return Ok(_apiResponse);
        }
    }
}
