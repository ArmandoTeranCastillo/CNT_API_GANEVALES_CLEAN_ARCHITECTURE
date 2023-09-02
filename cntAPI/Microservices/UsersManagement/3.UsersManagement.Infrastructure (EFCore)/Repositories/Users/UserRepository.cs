using _2.UsersManagement.Application.Interfaces.Users.Consults.Repositories;
using _3.UsersManagement.Infrastructure__EFCore_.Persistence;
using _3.UsersManagement.Infrastructure__EFCore_.Services;

namespace _3.UsersManagement.Infrastructure__EFCore_.Repositories.Users
{
    public class UserRepository : GenericService<_1.UsersManagement.Domain.Models.Users.Users>, IUserRepository
    {
        public UserRepository(CNTContext cnt) : base(cnt) { }
    }
}
