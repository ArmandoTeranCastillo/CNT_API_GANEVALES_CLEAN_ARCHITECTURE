using System.Net;
using System.Threading.Tasks;
using _2.Web.Gateway.Application.Handlers;
using _2.Web.Gateway.Application.Transients;
using _3.Web.Gateway.Infrastructure__EFCore_.Services.Urls.Mappings;
using Web.Gateway.Common.Errors;
using Web.Gateway.Common.Utilities;

namespace _3.Web.Gateway.Infrastructure__EFCore_.Handlers
{
    public class AddressesHandler : IAddressesHandler
    {
        private readonly IServiceUnit _sUnit;
        public AddressesHandler(IServiceUnit sUnit)
        {
            _sUnit = sUnit;
        }

        public Task<object> HandleGetAll(string controllerName, string entity, string userId)
        {
            throw new System.NotImplementedException();
        }

        public async Task<object> HandleGetAllById(string controllerName, string entity, string reference, string userId)
        {
            var dto = RequestMappings.FillRequestDto(Hosts.UsersManagement, NUrls.GetAllById, userId, controllerName, entity, reference);
            return entity.ToLower() switch
            {
                "countries" => await _sUnit.Request.SendGetRequest(dto),
                "states" => await _sUnit.Request.SendGetRequest(dto),
                "municipalities" => await _sUnit.Request.SendGetRequest(dto),
                "cities" => await _sUnit.Request.SendGetRequest(dto),
                "zipcodes" => await _sUnit.Request.SendGetRequest(dto),
                "addressesbyzipcode" => await _sUnit.Request.SendGetRequest(dto),
                "addressesbyidzipcode" => await _sUnit.Request.SendGetRequest(dto),
                "addressesbyuser" => await _sUnit.Request.SendGetRequest(dto),
                _ => throw new NotFoundException(HttpStatusCode.NotFound, Codes.EmptyField, "Empty Field")
            };
        }

        public async Task<object> HandleGetSimpleById(string controllerName, string entity, string reference, string userid)
        {
            var dto = RequestMappings.FillRequestDto(Hosts.UsersManagement, NUrls.GetSimpleById, userid, controllerName, entity, reference);
            return entity.ToLower() switch
            {
                "country" => await _sUnit.Request.SendGetRequest(dto),
                "state" => await _sUnit.Request.SendGetRequest(dto),
                _ => throw new NotFoundException(HttpStatusCode.NotFound, Codes.EmptyField, "Empty Field")
            };
        }

        public async Task<object> HandleGetAllOneField(string controllerName, string entity, string userid)
        {
            var dto = RequestMappings.FillRequestDto(Hosts.UsersManagement, NUrls.GetAllOneField, userid, controllerName, entity);
            return entity.ToLower() switch
            {
                "countries" => await _sUnit.Request.SendGetRequest(dto),
                _ => throw new NotFoundException(HttpStatusCode.NotFound, Codes.EmptyField, "Empty Field")
            };
        }

        public async Task<object> HandleGetSimpleOneFieldById(string controllerName, string entity, string reference, string userid)
        {
            var dto = RequestMappings.FillRequestDto(Hosts.UsersManagement, NUrls.GetSimpleOneField, userid, controllerName, entity, reference);
            return entity.ToLower() switch
            {
                "zipcode" => await _sUnit.Request.SendGetRequest(dto),
                _ => throw new NotFoundException(HttpStatusCode.NotFound, Codes.EmptyField, "Empty Field")
            };
        }

        public async Task<object> HandleInsertGeneric(string controllerName, string entity, object request, string userid)
        {
            var dto = RequestMappings.FillRequestDto(Hosts.UsersManagement, NUrls.Insert, userid, controllerName, entity, null, request);
            return entity.ToLower() switch
            {
                "address" => await _sUnit.Request.SendPostRequest(dto),
                _ => throw new NotFoundException(HttpStatusCode.NotFound, Codes.EmptyField, "Empty Field")
            };
        }

        public async Task<object> HandleUpdateGeneric(string controllerName, string entity, object request, string userid)
        {
            var dto = RequestMappings.FillRequestDto(Hosts.UsersManagement, NUrls.Update, userid, controllerName, entity, null, request);
            return entity.ToLower() switch
            {
                "address" => await _sUnit.Request.SendPostRequest(dto),
                _ => throw new NotFoundException(HttpStatusCode.NotFound, Codes.EmptyField, "Empty Field")
            };
        }
    }
}