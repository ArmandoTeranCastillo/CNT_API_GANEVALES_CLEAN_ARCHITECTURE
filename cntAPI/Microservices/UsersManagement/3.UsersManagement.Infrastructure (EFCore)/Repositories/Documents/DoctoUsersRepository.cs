using _1.UsersManagement.Domain.Models.Documents;
using _2.UsersManagement.Application.Interfaces.Documents.Repositories;
using _3.UsersManagement.Infrastructure__EFCore_.Persistence;
using _3.UsersManagement.Infrastructure__EFCore_.Services;

namespace _3.UsersManagement.Infrastructure__EFCore_.Repositories.Documents
{
    public class DoctoUsersRepository : GenericService<DoctoUsers>, IDoctoUsersRepository
    {
        public DoctoUsersRepository(CNTContext cnt) : base(cnt) { }
    }
}