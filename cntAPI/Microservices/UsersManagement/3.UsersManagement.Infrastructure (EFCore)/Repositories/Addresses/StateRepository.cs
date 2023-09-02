using _1.UsersManagement.Domain.Models.Addresses;
using _2.UsersManagement.Application.Interfaces.Addresses.Repositories;
using _3.UsersManagement.Infrastructure__EFCore_.Persistence;
using _3.UsersManagement.Infrastructure__EFCore_.Services;

namespace _3.UsersManagement.Infrastructure__EFCore_.Repositories.Addresses
{
    public class StateRepository : GenericService<States>, IStateRepository
    {
        public StateRepository(CNTContext cnt) : base(cnt) { }
    }
}
