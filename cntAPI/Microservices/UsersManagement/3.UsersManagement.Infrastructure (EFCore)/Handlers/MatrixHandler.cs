using System.Threading.Tasks;
using _2.UsersManagement.Application.Handlers;
using _2.UsersManagement.Application.Transients;
using UsersManagement.Common.Errors;
using UsersManagement.Common.Utilities;

namespace _3.UsersManagement.Infrastructure__EFCore_.Handlers
{
    public class MatrixHandler : IMatrixHandler
    {
        private readonly IGenericUnit _gUnit;
        private readonly IServiceUnit _sUnit;
        public MatrixHandler(IGenericUnit gUnit, IServiceUnit sUnit)
        {
            _gUnit = gUnit;
            _sUnit = sUnit;
        }
        public async Task<object> HandleGetAll(string entity)
        {
            return entity.ToLower() switch
            {
                "status" => await _gUnit.Matriz.GetAllById(i => i.Order, i => i.id_matriz_type == Value.MatrixStatus),
                "files" => await _gUnit.Matriz.GetAllById(i => i.Order, i => i.id_matriz_type == Value.DocType),
                _ => throw new NotFoundException(Codes.EmptyField)
            };
        }
        
        public Task<object> HandleGetAllById(string entity, string reference)
        {
            throw new System.NotImplementedException();
        }

        public Task<object> HandleGetSimpleById(string entity, string reference)
        {
            throw new System.NotImplementedException();
        }

        public Task<object> HandleGetAllOneField(string entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<object> HandleGetSimpleOneFieldById(string entity, string reference)
        {
            throw new System.NotImplementedException();
        }

        public Task<object> HandleInsertGeneric(string entity, object request)
        {
            throw new System.NotImplementedException();
        }

        public Task<object> HandleUpdateGeneric(string entity, object request)
        {
            throw new System.NotImplementedException();
        }
    }
}