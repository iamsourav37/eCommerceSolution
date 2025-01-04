﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Core.Domain
{
    public class Account : IdentityUser<Guid>
    {
        public string FullName { get; set; }

        public Customer? CustomerDetails { get; set; }
    }
}