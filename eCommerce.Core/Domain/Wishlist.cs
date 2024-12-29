using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Core.Domain
{
    public class Wishlist
    {
        public Guid ProductId { get; set; }
        public Product Product { get; set; }

        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedDate { get; set; } = DateTime.UtcNow;
    }
}
