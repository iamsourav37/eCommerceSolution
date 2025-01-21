using eCommerce.Core.DTOs.Product;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Core.DTOs.LineItemDTO
{
    public class LineItemCreateDto
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
