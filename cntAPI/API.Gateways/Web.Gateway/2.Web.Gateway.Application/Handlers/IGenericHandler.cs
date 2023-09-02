using System.Threading.Tasks;

namespace _2.Web.Gateway.Application.Handlers
{
    public interface IGenericHandler
    {
        Task<object> HandleGetAll(string controllerName, string entity, string userId);
        Task<object> HandleGetAllById(string controllerName, string entity, string reference, string userid);
        Task<object> HandleGetSimpleById(string controllerName, string entity, string reference, string userid);
        Task<object> HandleGetAllOneField(string controllerName, string entity, string userid);
        Task<object> HandleGetSimpleOneFieldById(string controllerName, string entity, string reference, string userid);
        Task<object> HandleInsertGeneric(string controllerName, string entity, object request, string userid);
        Task<object> HandleUpdateGeneric(string controllerName, string entity, object request, string userid);
    }
}