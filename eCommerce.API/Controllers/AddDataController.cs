using eCommerce.Core.Domain;
using eCommerce.Infrastructure.DbContext;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddDataController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public AddDataController(ApplicationDbContext dbContext, UserManager<Customer> userManager)
        {
            this._dbContext = dbContext;
        }

        [HttpPost("add-customer")]
        public IActionResult AddCustomer()
        {


            return Ok();
        }

        [HttpPost("add-product")]
        public IActionResult AddProduct()
        {


            return Ok();
        }


        [HttpPost("add-order")]
        public IActionResult AddOrder()
        {


            return Ok();
        }
    }
}
