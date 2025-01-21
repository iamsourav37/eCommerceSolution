using eCommerce.Core.Domain;
using eCommerce.Core.DTOs.AddressDTO;
using eCommerce.Core.DTOs.CustomerDTO;
using eCommerce.Core.DTOs.LineItemDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Core.DTOs.OrderDTO
{
    public class OrderDto
    {
        public Guid Id { get; set; }
        public double TotalAmount { get; set; }
        public DateTime OrderDate { get; set; } 
        public OrderStatus Status { get; set; }
        public PaymentStatus PaymentStatus { get; set; }

        // Line Item Information
        public ICollection<LineItemDto> LineItems { get; set; }


        // Customer Information
        public CustomerDto CustomerDto { get; set; }

        // Shipping Information
        public AddressDto ShippingAddress { get; set; }


        public DateTime CreatedDate { get; set; } 
        public DateTime UpdatedDate { get; set; } 
    }
}
