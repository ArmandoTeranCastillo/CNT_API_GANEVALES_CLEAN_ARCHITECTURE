using _1.UsersManagement.Domain.Models.Exceptions;
using _2.UsersManagement.Application.Interfaces.Exceptions.Repositories;
using _3.UsersManagement.Infrastructure__EFCore_.Persistence;
using _3.UsersManagement.Infrastructure__EFCore_.Services;

namespace _3.UsersManagement.Infrastructure__EFCore_.Repositories.Exceptions
{
    public class ErrorCodeRepository : GenericService<ErrorCodes>, IErrorCodeRepository
    {
        public ErrorCodeRepository(CNTContext cnt) : base(cnt) { }
    }
}