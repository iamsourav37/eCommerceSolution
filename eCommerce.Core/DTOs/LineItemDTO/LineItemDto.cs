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

        public ICollection<ProductDto> Products { get; set; }

        public double TotalPrice { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
