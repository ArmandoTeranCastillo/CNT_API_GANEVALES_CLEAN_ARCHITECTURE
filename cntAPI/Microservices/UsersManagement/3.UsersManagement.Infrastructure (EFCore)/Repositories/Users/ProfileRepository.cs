using _1.UsersManagement.Domain.Models.Users;
using _2.UsersManagement.Application.Interfaces.Users.Consults.Repositories;
using _3.UsersManagement.Infrastructure__EFCore_.Persistence;
using _3.UsersManagement.Infrastructure__EFCore_.Services;

namespace _3.UsersManagement.Infrastructure__EFCore_.Repositories.Users
{
    public class ProfileRepository : GenericService<Profiles>, IProfileRepository
    {
        public ProfileRepository(CNTContext cnt) : base(cnt) { }
    }
}
