using _2.Web.Gateway.Application.Handlers;

namespace _2.Web.Gateway.Application.Transients
{
    public interface IHandlerUnit : IGenericHandler
    {
        IUsersHandler Users { get; }
        IAddressesHandler Addresses { get; }
        IDistributorsHandler Distributors { get; }
        IPhonesHandler Phones { get; }
        ITasksHandler Tasks { get; }
        IMatrixHandler Matrix { get; }
    }
}