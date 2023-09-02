using System.Threading.Tasks;

namespace _2.UsersManagement.Application.Handlers
{
    public interface IGenericHandler
    {
        Task<object> HandleGetAll(string entity);
        Task<object> HandleGetAllById(string entity, string reference);
        Task<object> HandleGetSimpleById(string entity, string reference);
        Task<object> HandleGetAllOneField(string entity);
        Task<object> HandleGetSimpleOneFieldById(string entity, string reference);
        Task<object> HandleInsertGeneric(string entity, object request);
        Task<object> HandleUpdateGeneric(string entity, object request);
    }
}