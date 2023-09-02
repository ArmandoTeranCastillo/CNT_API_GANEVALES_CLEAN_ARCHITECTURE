using _1.UsersManagement.Domain.Models.Documents;
using _2.UsersManagement.Application.Interfaces.Documents.Repositories;
using _3.UsersManagement.Infrastructure__EFCore_.Persistence;
using _3.UsersManagement.Infrastructure__EFCore_.Services;

namespace _3.UsersManagement.Infrastructure__EFCore_.Repositories.Documents
{
    public class DoctoReqsRepository : GenericService<DoctoReqs>, IDoctoReqsRepository
    {
        public DoctoReqsRepository(CNTContext cnt) : base(cnt) { }
    }
}