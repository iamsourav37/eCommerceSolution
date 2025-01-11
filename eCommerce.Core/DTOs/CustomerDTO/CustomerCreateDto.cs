using eCommerce.Core.DTOs.AddressDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Core.DTOs.CustomerDTO
{
    public class CustomerCreateDto
    {
        public string Name { get; set; }
        public Guid AccountId { get; set; }
        public AddressCreateDto? Address{ get; set; }
    }
}
