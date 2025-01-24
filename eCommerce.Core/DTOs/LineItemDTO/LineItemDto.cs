using eCommerce.Core.DTOs.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Core.DTOs.LineItemDTO
{
    public class LineItemDto
    {
        public Guid Id { get; set; }

        public ProductDto Product { get; set; }
        public int Quantity { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
