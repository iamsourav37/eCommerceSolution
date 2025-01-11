using eCommerce.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Core.DTOs.CustomerDTO
{
    public class CustomerDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<Address>? Addresses { get; set; }
        public ICollection<Order>? Orders { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
