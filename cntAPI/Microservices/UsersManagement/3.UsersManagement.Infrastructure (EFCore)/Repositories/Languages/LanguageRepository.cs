using _1.UsersManagement.Domain.Models.Language;
using _2.UsersManagement.Application.Interfaces.Languages.Repositories;
using _3.UsersManagement.Infrastructure__EFCore_.Persistence;
using _3.UsersManagement.Infrastructure__EFCore_.Services;

namespace _3.UsersManagement.Infrastructure__EFCore_.Repositories.Languages
{
    public class LanguageRepository : GenericService<_1.UsersManagement.Domain.Models.Language.Languages>, ILanguageRepository
    {
        public LanguageRepository(CNTContext cnt) : base(cnt) { }
    }
}