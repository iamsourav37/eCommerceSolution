using eCommerce.API.Utility;
using eCommerce.API.ViewModels;
using eCommerce.Core.Domain;
using eCommerce.Core.DTOs.AuthDto;
using eCommerce.Core.DTOs.CustomerDTO;
using eCommerce.Core.Interfaces.RepositoryContracts;
using eCommerce.Core.Interfaces.ServiceContracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<Account> _userManager;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;
        private readonly ITokenRepository _tokenRepository;
        private readonly ICustomerService _customerService;
        private ApiResponse _response;

        public AuthController(UserManager<Account> userManager, ITokenRepository tokenRepository, ICustomerService customerService, RoleManager<IdentityRole<Guid>> roleManager)
        {
            _response = new ApiResponse();
            this._userManager = userManager;
            this._roleManager = roleManager;
            this._tokenRepository = tokenRepository;
            this._customerService = customerService;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDTO)
        {

            var identityUser = new Account()
            {
                UserName = registerDTO.Email,
                Email = registerDTO.Email,
                FullName = registerDTO.FullName,
                NormalizedEmail = registerDTO.Email.ToUpper(),
                NormalizedUserName = registerDTO.Email.ToUpper()
            };

            var identityResult = await _userManager.CreateAsync(identityUser, registerDTO.Password);
            if (identityResult.Succeeded)
            {
                var identityRole = await _roleManager.FindByNameAsync(Constants.CUSTOMER_ROLE);
                if (identityRole != null)
                {
                    await _userManager.AddToRoleAsync(identityUser, Constants.ADMIN_ROLE);
                }

                // Now create the Customer
                var customerCreateDto = new CustomerCreateDto()
                {
                    Name = registerDTO.FullName,
                    AccountId = identityUser.Id,
                };

                var createdCustomer = await this._customerService.CreateCustomerAsync(customerCreateDto);

                if (createdCustomer == null)
                {
                    _response.SetResponse(false, 400, "Failed to register!", null);
                    return BadRequest(_response);
                }

                _response.SetResponse(true, 201, "User Registered Successfully!", null);
                return Ok(_response);

            }

            #region Failed to create the Account
            var errorList = new List<string>();
            foreach (var error in identityResult.Errors)
            {
                errorList.Add(error.Description);
            }
            _response.SetResponse(false, 400, null, errorList.ToArray());
            return BadRequest(_response);
            #endregion
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user != null)
            {
                var isValidPassword = await _userManager.CheckPasswordAsync(user, loginDto.Password);

                if (isValidPassword)
                {
                    var roles = await _userManager.GetRolesAsync(user);
                    // Create the JWT Token
                    var token = _tokenRepository.GenerateToken(user, roles.ToList());
                   

                    var customerDetails = await _customerService.GetCustomerIdByAccountId(user.Id);

                    LoginResponse loginViewModel = new LoginResponse()
                    {
                        CustomerName = customerDetails.Name,
                        CustomerId = customerDetails.Id,
                        Email = user.Email,
                        JwtToken = new JwtTokenDetails()
                        {
                            AuthToken = token,
                            ExpiresInHours = Constants.TOKEN_EXPIRES_TIME
                        }
                    };


                    _response.SetResponse(true, 200, loginViewModel, null);
                    return Ok(_response);
                }

            }
            _response.SetResponse(false, 400, null, ["Login Failed, Check the username and password"]);
            return BadRequest(_response);
        }
    }
}
