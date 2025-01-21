using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace eCommerce.Core.Domain
{
    public enum OrderStatus
    {
        Pending,
        Completed,
        Cancelled
    }

    public enum PaymentStatus
    {
        Paid,
        Unpaid,
        Refunded
    }
    public class Order
    {
        public Guid Id { get; set; }
        public double TotalAmount { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public PaymentStatus PaymentStatus { get; set; }


        // Line Item Information
        public ICollection<LineItem> LineItems { get; set; }


        // Customer Information
        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; }

        // Shipping Information
        public Guid ShippingAddressId { get; set; }
        public Address ShippingAddress { get; set; }


        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedDate { get; set; } = DateTime.UtcNow;
    }
}
