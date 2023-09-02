using _1.UsersManagement.Domain.Models.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2.UsersManagement.Application.Interfaces.Tasks.Consults.Repositories;
using _3.UsersManagement.Infrastructure__EFCore_.Persistence;
using _3.UsersManagement.Infrastructure__EFCore_.Services;

namespace _3.UsersManagement.Infrastructure__EFCore_.Repositories.Tasks
{
    public class TaskRepository : GenericService<_1.UsersManagement.Domain.Models.Tasks.Tasks>, ITaskRepository
    {
        public TaskRepository(CNTContext cnt) : base(cnt) { }
    }
}
