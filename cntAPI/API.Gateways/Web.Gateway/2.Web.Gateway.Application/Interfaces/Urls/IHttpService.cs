using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace _2.Web.Gateway.Application.Interfaces.Urls
{
    public interface IHttpService
    {
        Task<dynamic> GetAsync<TResponse>(string param);
        Task<byte[]> GetImageAsync(string url);
        Task<dynamic> PostAsync<TResponse, TRequest>(string param, TRequest request);
        Task<dynamic> PostMultipartAsync<TResponse>(string url, string json, List<IFormFile> files);
        Task<dynamic> PutAsync<TResponse, TRequest>(string param, TRequest request);
    }
}
