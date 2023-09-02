using System.Threading.Tasks;
using _2.UsersManagement.Application.DTOs.Tasks;
using _2.UsersManagement.Application.Handlers;
using _2.UsersManagement.Application.Transients;
using _3.UsersManagement.Infrastructure__EFCore_.External;
using Newtonsoft.Json;
using UsersManagement.Common.Errors;
using UsersManagement.Common.Utilities;

namespace _3.UsersManagement.Infrastructure__EFCore_.Handlers
{
    public class TasksHandler : ITasksHandler
    {
        private readonly IGenericUnit _gUnit;
        private readonly IServiceUnit _sUnit;
        public TasksHandler(IGenericUnit gUnit, IServiceUnit sUnit)
        {
            _gUnit = gUnit;
            _sUnit = sUnit;
        }
        
        public Task<object> HandleGetAll(string entity)
        {
            throw new System.NotImplementedException();
        }
        
        public async Task<object> HandleGetAllById(string entity, string reference)
        {
            return entity switch
            {
                "Actions" => await _sUnit.Tasks.GetAllCompleteActions(reference),
                _ => throw new NotFoundException(Codes.EmptyField)
            };
        }

        public Task<object> HandleGetSimpleById(string entity, string reference)
        {
            return entity switch
            {
                "Action" => _sUnit.Tasks.GetSimpleCompleteActions(reference),
                _ => throw new NotFoundException(Codes.EmptyField)
            };
        }

        public Task<object> HandleGetAllOneField(string entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<object> HandleGetSimpleOneFieldById(string entity, string reference)
        {
            throw new System.NotImplementedException();
        }

        public async Task<object> HandleInsertGeneric(string entity, object request)
        {
            return entity.ToLower() switch
            {
                "task" => await _sUnit.Tasks.InsertTask(Json.Deserialize<InsertTaskDto>(request)),
                _ => throw new NotFoundException(Codes.EmptyField)
            };
        }

        public async Task<object> HandleUpdateGeneric(string entity, object request)
        {
            return entity.ToLower() switch
            {
                "task" => await _gUnit.Task.UpdateEntity(Json.Deserialize<UpdateTaskDto>(request)),
                _ => throw new NotFoundException(Codes.EmptyField)
            };
        }
    }
}