using _1.UsersManagement.Domain.Models.Tasks;
using _2.UsersManagement.Application.Interfaces.Tasks.Consults.Repositories;
using _3.UsersManagement.Infrastructure__EFCore_.Persistence;
using _3.UsersManagement.Infrastructure__EFCore_.Services;

namespace _3.UsersManagement.Infrastructure__EFCore_.Repositories.Tasks
{
    public class TaskMessageRepository : GenericService<TaskMessages>, ITaskMessageRepository
    {
        public TaskMessageRepository(CNTContext cnt) : base(cnt) { }
    }
}