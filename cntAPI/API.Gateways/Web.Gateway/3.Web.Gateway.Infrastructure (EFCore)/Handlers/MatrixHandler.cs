using System.Net;
using System.Threading.Tasks;
using _2.Web.Gateway.Application.Handlers;
using _2.Web.Gateway.Application.Transients;
using _3.Web.Gateway.Infrastructure__EFCore_.Services.Urls.Mappings;
using Web.Gateway.Common.Errors;
using Web.Gateway.Common.Utilities;

namespace _3.Web.Gateway.Infrastructure__EFCore_.Handlers
{
    public class MatrixHandler : IMatrixHandler
    {
        private readonly IServiceUnit _sUnit;
        public MatrixHandler(IServiceUnit sUnit)
        {
            _sUnit = sUnit;
        }
        public async Task<object> HandleGetAll(string controllerName, string entity, string userId)
        {
            var dto = RequestMappings.FillRequestDto(Hosts.UsersManagement, NUrls.GetAll, userId, controllerName, entity);
            return entity.ToLower() switch
            {
                "status" => await _sUnit.Request.SendGetRequest(dto),
                "files" => await _sUnit.Request.SendGetRequest(dto),
                _ => throw new NotFoundException(HttpStatusCode.NotFound, Codes.EmptyField, "Empty Field")
            };
        }

        public Task<object> HandleGetAllById(string controllerName, string entity, string reference, string userid)
        {
            throw new System.NotImplementedException();
        }

        public Task<object> HandleGetSimpleById(string controllerName, string entity, string reference, string userid)
        {
            throw new System.NotImplementedException();
        }

        public Task<object> HandleGetAllOneField(string controllerName, string entity, string userid)
        {
            throw new System.NotImplementedException();
        }

        public Task<object> HandleGetSimpleOneFieldById(string controllerName, string entity, string reference, string userid)
        {
            throw new System.NotImplementedException();
        }

        public Task<object> HandleInsertGeneric(string controllerName, string entity, object request, string userid)
        {
            throw new System.NotImplementedException();
        }

        public Task<object> HandleUpdateGeneric(string controllerName, string entity, object request, string userid)
        {
            throw new System.NotImplementedException();
        }
    }
}