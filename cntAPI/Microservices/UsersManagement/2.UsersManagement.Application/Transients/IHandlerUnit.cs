using _2.UsersManagement.Application.Handlers;

namespace _2.UsersManagement.Application.Transients
{
    public interface IHandlerUnit : IGenericHandler
    {
        IUsersHandler Users { get; }
        IAddressesHandler Addresses { get; }
        IDistributorsHandler Distributors { get; }
        IPhonesHandler Phones { get; }
        IDocumentsHandler Documents { get; }
        ITasksHandler Tasks { get; }
        IMatrixHandler Matrix { get; }
    }
}