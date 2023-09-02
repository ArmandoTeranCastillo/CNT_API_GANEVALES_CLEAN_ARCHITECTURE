using System.Threading.Tasks;
using _2.Web.Gateway.Application.Handlers;
using _2.Web.Gateway.Application.Transients;
using _3.Web.Gateway.Infrastructure__EFCore_.Handlers;

namespace _3.Web.Gateway.Infrastructure__EFCore_.Transients
{
    public class HandlerUnit : IHandlerUnit
    {
        public HandlerUnit(IServiceUnit sUnit)
        {
            Users = new UsersHandler(sUnit);
            Addresses = new AddressesHandler(sUnit);
            Distributors = new DistributorsHandler(sUnit);
            Phones = new PhonesHandler(sUnit);
            Tasks = new TasksHandler(sUnit);
            Matrix = new MatrixHandler(sUnit);
        }

        public IUsersHandler Users { get; private set; }
        public IAddressesHandler Addresses { get; private set; }
        public IDistributorsHandler Distributors { get; private set; }
        public IPhonesHandler Phones { get; private set; }
        public ITasksHandler Tasks { get; private set; }
        public IMatrixHandler Matrix { get; private set; }
        
        public Task<object> HandleGetAll(string controllerName, string entity, string userId)
        {
            throw new System.NotImplementedException();
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