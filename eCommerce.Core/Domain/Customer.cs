using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace eCommerce.Core.Domain
{
    public class Customer
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid AccountId { get; set; }

        public ICollection<Address>? Addresses { get; set; }
        [JsonIgnore]
        public ICollection<Order>? Orders { get; set; }

        [ForeignKey(nameof(AccountId))]
        [JsonIgnore]
        public Account Account { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedDate { get; set; } = DateTime.UtcNow;
    }
}
