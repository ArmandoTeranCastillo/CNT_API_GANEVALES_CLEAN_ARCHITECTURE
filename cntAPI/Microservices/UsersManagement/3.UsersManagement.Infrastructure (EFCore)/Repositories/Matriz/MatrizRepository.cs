using _2.UsersManagement.Application.Interfaces.Matriz.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1.UsersManagement.Domain.Models.Matrices;
using _3.UsersManagement.Infrastructure__EFCore_.Persistence;
using _3.UsersManagement.Infrastructure__EFCore_.Services;

namespace _3.UsersManagement.Infrastructure__EFCore_.Repositories.Matriz
{
    public class MatrizRepository : GenericService<Matrices>, IMatrizRepository
    {
        public MatrizRepository(CNTContext cnt) : base(cnt) { }
    }
}
