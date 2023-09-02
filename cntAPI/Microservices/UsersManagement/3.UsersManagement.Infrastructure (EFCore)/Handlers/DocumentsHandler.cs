using System.Threading.Tasks;
using _2.UsersManagement.Application.Handlers;
using _2.UsersManagement.Application.Transients;
using UsersManagement.Common.Errors;
using UsersManagement.Common.Utilities;

namespace _3.UsersManagement.Infrastructure__EFCore_.Handlers
{
    public class DocumentsHandler : IDocumentsHandler
    {
        private readonly IGenericUnit _gUnit;
        private readonly IServiceUnit _sUnit;
        public DocumentsHandler(IGenericUnit gUnit, IServiceUnit sUnit)
        {
            _gUnit = gUnit;
            _sUnit = sUnit;
        }

        public async Task<object> HandleGetAll(string entity)
        {
            return entity.ToLower() switch
            {
                "doctotypes" => await _sUnit.Documents.GetAllCompleteDoctoTypes(),
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