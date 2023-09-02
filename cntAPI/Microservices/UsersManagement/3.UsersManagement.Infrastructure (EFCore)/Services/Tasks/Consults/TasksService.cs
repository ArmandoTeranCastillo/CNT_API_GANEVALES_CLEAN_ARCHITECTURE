using System.Linq;
using System.Threading.Tasks;
using _2.UsersManagement.Application.DTOs.Tasks;
using _2.UsersManagement.Application.Interfaces.Tasks.Consults;
using _2.UsersManagement.Application.Transients;
using _3.UsersManagement.Infrastructure__EFCore_.Persistence;
using _3.UsersManagement.Infrastructure__EFCore_.Services.Tasks.Mappings;
using Microsoft.EntityFrameworkCore;
using UsersManagement.Common.Utilities;

namespace _3.UsersManagement.Infrastructure__EFCore_.Services.Tasks.Consults
{
    public class TasksService : ITasksService
    {
        private readonly CNTContext _cnt;
        private readonly IGenericUnit _gUnit;
        public TasksService(CNTContext cnt, IGenericUnit gUnit)
        {
            _cnt = cnt;
            _gUnit = gUnit;
        }

        public async Task<object> GetAllCompleteActions(string id)
        {
            return await _cnt.Tasks
                .Where(w => w.IdUser == id)
                .Include(i => i.TasksType)
                .Include(i => i.Matrices) 
                .ToListAsync();
        }

        public async Task<object> GetSimpleCompleteActions(string id)
        {
            return await _cnt.Tasks
                .Where(w => w.Id == id)
                .Include(i => i.TasksType)
                .Include(i => i.Matrices)
                .FirstOrDefaultAsync();
        }

        public async Task<object> InsertTask(InsertTaskDto request)
        {
            var task = TasksMappings.FillModelTask(request);
            task.IdTaskType = Value.ActionTaskType;
            _cnt.Tasks.Add(task);
            await _cnt.SaveChangesAsync();
            return task;
        }
    }
}