using eCommerce.Core.Domain;
using eCommerce.Core.DTOs.LineItemDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Core.DTOs.OrderDTO
{
    public class OrderCreateDto
    {
        public PaymentStatus PaymentStatus { get; set; }

        // Line Item Information
        public ICollection<LineItemCreateDto> LineItems { get; set; }

        // Customer Information
        public Guid CustomerId { get; set; }

        // Shipping Information
        public Guid ShippingAddressId { get; set; }
    }
}
