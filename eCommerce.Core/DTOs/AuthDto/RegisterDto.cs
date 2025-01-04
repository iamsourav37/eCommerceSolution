using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Core.DTOs.AuthDto
{
    public class RegisterDto
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string AddressLine1 { get; set; }
        public string? AddressLine2 { get; set; }
        public string? Landmark { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string? PinCode { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
