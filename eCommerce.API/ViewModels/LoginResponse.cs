namespace eCommerce.API.ViewModels
{
    public class LoginResponse
    {
        public Guid CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string Email { get; set; }
        public JwtTokenDetails JwtToken { get; set; }
    }

    public class JwtTokenDetails
    {
        public string AuthToken { get; set; }
        public int ExpiresInHours { get; set; }
    }
}
