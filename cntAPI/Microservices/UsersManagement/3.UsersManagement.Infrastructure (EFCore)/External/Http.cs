using Microsoft.AspNetCore.Http;

namespace _3.UsersManagement.Infrastructure__EFCore_.External
{
    public class Http
    {
        public static void CleanCache(HttpResponse response)
        {
            response.Headers.Add("Cache-Control", "no-store, no-cache, must-revalidate, post-check=0, pre-check=0");
            response.Headers.Add("Pragma", "no-cache");
            response.Headers.Add("Expires", "0");
        }
    }
}