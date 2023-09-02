using System.Collections.Generic;
using System.Threading.Tasks;
using _2.Web.Gateway.Application.DTOs;
using _2.Web.Gateway.Application.Interfaces.Urls;
using Web.Gateway.Common.Utilities;

namespace _3.Web.Gateway.Infrastructure__EFCore_.Services.Urls
{
    public class RequestService : IRequestService
    {
        private readonly IHttpService _http;
        private readonly IUrlsService _urls;
        public RequestService(IHttpService http, IUrlsService urls)
        {
            _http = http;
            _urls = urls;
        }

        //----------------------------GENERIC CONTROLLER-----------------------------//
        public async Task<object> SendGetRequest(SendRequestDto request)
        {
            var param = new Dictionary<string, object>
            {
                {"controller", request.ControllerName},
                {"entity", request.Entity},
                {"userid", request.UserId},
                {"host", request.Host},
                {"reference", request.Reference},
                {"language", request.Language}
            };
            var url = _urls.GetUrl(request.Url, param);
            return await _http.GetAsync<object>(url);
        }

        public async Task<object> SendPostRequest(SendRequestDto request)
        {
            var param = new Dictionary<string, object>
            {
                {"controller", request.ControllerName},
                {"entity", request.Entity},
                {"userId", request.UserId},
                {"host", request.Host}
            };
            var url = _urls.GetUrl(request.Url, param);
            return await _http.PostAsync<object, object>(url, request.Request);
        }

        public async Task<object> SendPutRequest(SendRequestDto request)
        {
            var param = new Dictionary<string, object>
            {
                {"controller", request.ControllerName},
                {"entity", request.Entity},
                {"userId", request.UserId},
                {"host", request.Host},
                {"reference", request.Reference}
            };
            var url = _urls.GetUrl(request.Url, param);
            return await _http.PutAsync<object, object>(url, request.Request);
        }
        
        //-----------------------------DOCUMENTS CONTROLLER-----------------------------------//
        public async Task<object> SendRegisterDocumentsRequest(string entity, object json, string subEntity)
        {
            object[] param = { entity, subEntity, Hosts.UsersManagement };
            var url = _urls.GetUrl(NUrls.RegisterDocuments, param);
            return await _http.PostAsync<object, object>(url, json);
        }
    }
}
