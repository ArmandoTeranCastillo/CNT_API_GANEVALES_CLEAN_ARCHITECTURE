using System.Threading.Tasks;
using _2.UsersManagement.Application.Handlers;
using _2.UsersManagement.Application.Interfaces.Documents;
using _2.UsersManagement.Application.Transients;
using _3.UsersManagement.Infrastructure__EFCore_.Handlers;

namespace _3.UsersManagement.Infrastructure__EFCore_.Transients
{
    public class HandlerUnit : IHandlerUnit
    {
        public HandlerUnit(IGenericUnit gUnit, IServiceUnit sUnit)
        {
            Users = new UsersHandler(gUnit, sUnit);
            Addresses = new AddressesHandler(gUnit, sUnit);
            Distributors = new DistributorsHandler(gUnit, sUnit);
            Phones = new PhonesHandler(gUnit, sUnit);
            Documents = new DocumentsHandler(gUnit, sUnit);
            Tasks = new TasksHandler(gUnit, sUnit);
            Matrix = new MatrixHandler(gUnit, sUnit);
        }

        public IUsersHandler Users { get; private set; }
        public IAddressesHandler Addresses { get; private set; }
        public IDistributorsHandler Distributors { get; private set; }
        public IPhonesHandler Phones { get; private set; }
        public IDocumentsHandler Documents { get; private set; }
        public ITasksHandler Tasks { get; private set; }
        public IMatrixHandler Matrix { get; private set; }

        public Task<object> HandleGetAll(string controller)
        {
            throw new System.NotImplementedException();
        }

        public Task<object> HandleGetAll(string entity, string iso)
        {
            throw new System.NotImplementedException();
        }

        public Task<object> HandleGetAllById(string entity, string reference)
        {
            throw new System.NotImplementedException();
        }

        public Task<object> HandleGetSimpleById(string entity, string reference)
        {
            throw new System.NotImplementedException();
        }

        public Task<object> HandleGetAllOneField(string controller)
        {
            throw new System.NotImplementedException();
        }

        public Task<object> HandleGetSimpleOneFieldById(string controller, string reference)
        {
            throw new System.NotImplementedException();
        }

        public Task<object> HandleInsertGeneric(string entity, object request)
        {
            throw new System.NotImplementedException();
        }

        public Task<object> HandleUpdateGeneric(string entity, object request)
        {
            throw new System.NotImplementedException();
        }
    }
}