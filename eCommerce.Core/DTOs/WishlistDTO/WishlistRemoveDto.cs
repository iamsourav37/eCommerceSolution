﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Core.DTOs.WishlistDTO
{
    public class WishlistRemoveDto
    {
        public Guid ProductId { get; set; }
        public Guid CustomerId { get; set; }
    }
}
