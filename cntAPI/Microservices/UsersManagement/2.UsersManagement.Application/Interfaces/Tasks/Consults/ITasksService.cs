using System.Threading.Tasks;
using _2.UsersManagement.Application.DTOs.Tasks;

namespace _2.UsersManagement.Application.Interfaces.Tasks.Consults
{
    public interface ITasksService
    {
        Task<object> GetAllCompleteActions(string id);
        Task<object> GetSimpleCompleteActions(string id);
        Task<object> InsertTask(InsertTaskDto request);
    }
}