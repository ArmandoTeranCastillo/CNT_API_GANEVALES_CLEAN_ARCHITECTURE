namespace _2.UsersManagement.Application.DTOs.Users.Auth.in_Services
{
    public class TokenDto
    {
        public string Token { get; set; }
        public string ExpiresIn { get; set; }
        public bool RememberMe { get; set; }
    }
}
