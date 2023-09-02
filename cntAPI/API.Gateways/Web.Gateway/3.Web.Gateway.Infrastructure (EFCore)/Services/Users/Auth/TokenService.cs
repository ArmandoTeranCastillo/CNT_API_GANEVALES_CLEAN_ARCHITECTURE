using _2.Web.Gateway.Application.Interfaces.Users.Auth;
using System;
using System.IdentityModel.Tokens.Jwt;

namespace _3.Web.Gateway.Infrastructure__EFCore_.Services.Users.Auth
{
    public class TokenService : ITokenService
    {
        public bool IsTokenExpired(string token)
        {
            var jwtToken = new JwtSecurityToken(token);
            var expirationTime = jwtToken.ValidTo;
            var isExpired = DateTime.UtcNow > expirationTime;
            return isExpired;
        }
    }
}
