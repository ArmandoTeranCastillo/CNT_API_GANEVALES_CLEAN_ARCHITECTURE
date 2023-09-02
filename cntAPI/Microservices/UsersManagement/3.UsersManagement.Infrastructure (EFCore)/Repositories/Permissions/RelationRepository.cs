using _1.UsersManagement.Domain.Models.Permissions;
using _2.UsersManagement.Application.Interfaces.Permissions.Repositories;
using _3.UsersManagement.Infrastructure__EFCore_.Persistence;
using _3.UsersManagement.Infrastructure__EFCore_.Services;

namespace _3.UsersManagement.Infrastructure__EFCore_.Repositories.Permissions
{
    public class RelationRepository : GenericService<Relations>, IRelationRepository
    {
        public RelationRepository(CNTContext cnt) : base(cnt) { }
    }
}
