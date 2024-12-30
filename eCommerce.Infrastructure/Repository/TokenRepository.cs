using eCommerce.Core.Interfaces.RepositoryContracts;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Infrastructure.Repository
{
    public class TokenRepository : ITokenRepository
    {
        public Task GenerateToken(IdentityUser user, List<string> roles)
        {
            throw new NotImplementedException();
        }
    }
}
