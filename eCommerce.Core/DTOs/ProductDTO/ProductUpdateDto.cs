using eCommerce.Core.DTOs.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Core.DTOs.ProductDTO
{
    public class ProductUpdateDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int? Quantity { get; set; }
        public bool IsActive { get; set; }

        public ICollection<CategoryDto>? Categories { get; set; }
    }
}
