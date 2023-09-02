using System.Net;
using System.Threading.Tasks;
using _2.Web.Gateway.Application.Handlers;
using _2.Web.Gateway.Application.Transients;
using _3.Web.Gateway.Infrastructure__EFCore_.Services.Urls.Mappings;
using Web.Gateway.Common.Errors;
using Web.Gateway.Common.Utilities;

namespace _3.Web.Gateway.Infrastructure__EFCore_.Handlers
{
    public class TasksHandler : ITasksHandler
    {
        private readonly IServiceUnit _sUnit;
        public TasksHandler(IServiceUnit sUnit)
        {
            _sUnit = sUnit;
        }
        
        public Task<object> HandleGetAll(string controllerName, string entity, string userId)
        {
            throw new System.NotImplementedException();
        }

        public async Task<object> HandleGetAllById(string controllerName, string entity, string reference, string userid)
        {
            var dto = RequestMappings.FillRequestDto(Hosts.TestUsersManagement, NUrls.GetAllById, userid, controllerName, entity, reference);
            return entity.ToLower() switch
            {
                "actions" => await _sUnit.Request.SendGetRequest(dto),
                _ => throw new NotFoundException(HttpStatusCode.NotFound, Codes.EmptyField, "Empty Field")
            };
        }

        public Task<object> HandleGetSimpleById(string controllerName, string entity, string reference, string userid)
        {
            var dto = RequestMappings.FillRequestDto(Hosts.UsersManagement, NUrls.GetSimpleById, userid, controllerName, entity, reference);
            return entity.ToLower() switch
            {
                "action" => _sUnit.Request.SendGetRequest(dto),
                _ => throw new NotFoundException(HttpStatusCode.NotFound, Codes.EmptyField, "Empty Field")
            };
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
            var dto = RequestMappings.FillRequestDto(Hosts.UsersManagement, NUrls.Insert, userid, controllerName, entity, null, request);
            return entity.ToLower() switch
            {
                "task" => _sUnit.Request.SendPostRequest(dto),
                _ => throw new NotFoundException(HttpStatusCode.NotFound, Codes.EmptyField, "Empty Field")
            };
        }

        public async Task<object> HandleUpdateGeneric(string controllerName, string entity, object request, string userid)
        {
            var dto = RequestMappings.FillRequestDto(Hosts.UsersManagement, NUrls.Update, userid, controllerName, entity, null, request);
            return entity.ToLower() switch
            {
                "task" => await _sUnit.Request.SendPostRequest(dto),
                _ => throw new NotFoundException(HttpStatusCode.NotFound, Codes.EmptyField, "Empty Field")
            };
        }
    }
}