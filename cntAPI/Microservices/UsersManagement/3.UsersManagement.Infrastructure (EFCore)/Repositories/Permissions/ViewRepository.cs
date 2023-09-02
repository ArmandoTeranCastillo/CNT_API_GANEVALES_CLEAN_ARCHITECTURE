using _1.UsersManagement.Domain.Models.Permissions;
using _2.UsersManagement.Application.Interfaces.Permissions.Repositories;
using _3.UsersManagement.Infrastructure__EFCore_.Persistence;
using _3.UsersManagement.Infrastructure__EFCore_.Services;

namespace _3.UsersManagement.Infrastructure__EFCore_.Repositories.Permissions
{
    public class ViewRepository : GenericService<Views>, IViewRepository
    {
        public ViewRepository(CNTContext cnt) : base(cnt) { }
    }
}