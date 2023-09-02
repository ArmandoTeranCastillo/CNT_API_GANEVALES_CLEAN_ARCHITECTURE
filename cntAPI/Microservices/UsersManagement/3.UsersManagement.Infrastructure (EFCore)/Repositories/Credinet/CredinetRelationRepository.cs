using _1.UsersManagement.Domain.Models.Credinet;
using _2.UsersManagement.Application.Interfaces.Credinet.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _3.UsersManagement.Infrastructure__EFCore_.Persistence;
using _3.UsersManagement.Infrastructure__EFCore_.Services;

namespace _3.UsersManagement.Infrastructure__EFCore_.Repositories.Credinet
{
    public class CredinetRelationRepository : GenericService<CredinetRelations>, ICredinetRelationRepository
    {
        public CredinetRelationRepository(CNTContext cnt) : base(cnt) { }
    }
}
