using System;
using Microsoft.AspNetCore.Http;

namespace _3.Web.Gateway.Infrastructure__EFCore_.External
{
    public class Http
    {
        public static void CleanCache(HttpResponse response)
        {
            response.Headers.Add("Cache-Control", "no-store, no-cache, must-revalidate, post-check=0, pre-check=0");
            response.Headers.Add("Pragma", "no-cache");
            response.Headers.Add("Expires", "0");
        }
        
        /*public static void SetRefreshToken(TokenDto refreshToken, HttpResponse response)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.Now.AddDays(1)
            };
            response.Cookies.Append("refreshToken", refreshToken.token, cookieOptions);
        }*/
        
        //isTokenExpired(Request.Cookies["refreshToken"]);
        
        /*private void IsTokenExpired(string token)
       {
           if (_sUnit.TokenService.IsTokenExpired(token))
           {
               throw new BadRequestException(HttpStatusCode.BadRequest, Codes.TokenExpired, "Token Expired");
           }
       }*/
    }
}