using System.Security.Claims;

namespace eCommerce.API.Helper
{
    public class UserHelper
    {

        public static Guid GetCurrentUserId(IHttpContextAccessor httpContextAccessor)
        {
            var userIdClaim = httpContextAccessor.HttpContext?.User.Claims
                .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

            return userIdClaim != null ? Guid.Parse(userIdClaim.Value) : Guid.Empty;
        }
    }
}
