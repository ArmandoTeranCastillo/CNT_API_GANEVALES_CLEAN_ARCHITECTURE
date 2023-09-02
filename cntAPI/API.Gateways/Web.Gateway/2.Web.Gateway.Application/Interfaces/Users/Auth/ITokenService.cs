namespace _2.Web.Gateway.Application.Interfaces.Users.Auth
{
    public interface ITokenService
    {
        bool IsTokenExpired(string token);
    }
}
