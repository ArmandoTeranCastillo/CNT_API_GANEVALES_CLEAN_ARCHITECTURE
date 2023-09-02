using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using _2.Web.Gateway.Application.Interfaces.Urls;
using Microsoft.AspNetCore.Http;
using Web.Gateway.Common.Errors;

namespace _3.Web.Gateway.Infrastructure__EFCore_.Services.Urls
{
    public class HttpService : IHttpService
    {
        public async Task<dynamic> GetAsync<TResponse>(string param)
        {
            using var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(param);
            var content = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                try
                {
                    return JsonSerializer.Deserialize<TResponse>(content);
                }
                catch (JsonException)
                {
                    return content;
                }
            }

            if (!HttpStatusExceptionMap.TryGetValue(response.StatusCode, out var exceptionType))
                throw new HttpRequestException($"Unexpected HTTP response: {response.StatusCode}");
            try
            {
                var jsonDocument = JsonSerializer.Deserialize<object>(content);
                throw ((Exception)Activator.CreateInstance(exceptionType, response.StatusCode, jsonDocument))!;
            }
            catch (JsonException)
            {
                throw new Exception(content);
            }
        }
        
        public async Task<byte[]> GetImageAsync(string url)
        {
            using var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsByteArrayAsync();
            }

            if (!HttpStatusExceptionMap.TryGetValue(response.StatusCode, out var exceptionType))
                throw new HttpRequestException($"Unexpected HTTP response: {response.StatusCode}");

            var content = await response.Content.ReadAsStringAsync();
            try
            {
                var jsonDocument = JsonSerializer.Deserialize<object>(content);
                throw ((Exception)Activator.CreateInstance(exceptionType, response.StatusCode, jsonDocument))!;
            }
            catch (JsonException)
            {
                throw new Exception(content);
            }
        }


        public async Task<dynamic> PostAsync<TResponse, TRequest>(string param, TRequest request)
        {
            using var httpClient = new HttpClient();
            var jsonRequest = JsonSerializer.Serialize(request);
            var httpContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync(param, httpContent);
            var content = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                try
                {
                    return JsonSerializer.Deserialize<TResponse>(content);
                }
                catch (JsonException)
                {
                    return content;
                }
            }
            if (!HttpStatusExceptionMap.TryGetValue(response.StatusCode, out var exceptionType))
                throw new HttpRequestException($"Unexpected HTTP response: {response.StatusCode}");
            try
            {
                var jsonDocument = JsonSerializer.Deserialize<object>(content);
                throw ((Exception)Activator.CreateInstance(exceptionType, response.StatusCode, jsonDocument))!;
            }
            catch (JsonException)
            {
                throw new Exception(content);
            }
        }
        
        public async Task<dynamic> PostMultipartAsync<TResponse>(string url, string json, List<IFormFile> files)
        {
            using var content = new MultipartFormDataContent();
            content.Add(new StringContent(json), "json");

            foreach (var file in files)
            {
                content.Add(new StreamContent(file.OpenReadStream())
                {
                    Headers =
                    {
                        ContentLength = file.Length,
                        ContentType = new MediaTypeHeaderValue(file.ContentType)
                    }
                }, "files", file.FileName);
            }

            using var httpClient = new HttpClient();
            var response = await httpClient.PostAsync(url, content);
            var responseContent = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                try
                {
                    return JsonSerializer.Deserialize<TResponse>(responseContent);
                }
                catch (JsonException)
                {
                    return responseContent;
                }
            }
            if (!HttpStatusExceptionMap.TryGetValue(response.StatusCode, out var exceptionType))
                throw new HttpRequestException($"Unexpected HTTP response: {response.StatusCode}");

            try
            {
                var jsonDocument = JsonSerializer.Deserialize<object>(responseContent);
                throw ((Exception)Activator.CreateInstance(exceptionType, response.StatusCode, jsonDocument))!;
            }
            catch (JsonException)
            {
                throw new Exception(responseContent);
            }
        }


        public async Task<dynamic> PutAsync<TResponse, TRequest>(string param, TRequest request)
        {
            using var httpClient = new HttpClient();
            var jsonRequest = JsonSerializer.Serialize(request);
            var httpContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

            var response = await httpClient.PutAsync(param, httpContent);
            var content = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                try
                {
                    return JsonSerializer.Deserialize<TResponse>(content);
                }
                catch (JsonException)
                {
                    return content;
                }
            }

            if (!HttpStatusExceptionMap.TryGetValue(response.StatusCode, out var exceptionType))
                throw new HttpRequestException($"Unexpected HTTP response: {response.StatusCode}");
            try
            {
                var jsonDocument = JsonSerializer.Deserialize<object>(content);
                throw ((Exception)Activator.CreateInstance(exceptionType, response.StatusCode, jsonDocument))!;
            }
            catch (JsonException)
            {
                throw new Exception(content);
            }
        }


        //-------------------------------------------PRIVATE------------------------------------------------------

        private static readonly Dictionary<HttpStatusCode, Type> HttpStatusExceptionMap = new()
        {
            { HttpStatusCode.NotFound, typeof(NotFoundException) },
            { HttpStatusCode.BadRequest, typeof(BadRequestException) },
            { HttpStatusCode.InternalServerError, typeof(InternalServerException) },
            { HttpStatusCode.ServiceUnavailable, typeof(ServiceUnavailable) }
        };
    }
}
