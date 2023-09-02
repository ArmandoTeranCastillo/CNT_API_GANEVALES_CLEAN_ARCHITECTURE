using System;
using System.Threading.Tasks;
using _2.UsersManagement.Application.DTOs.Exceptions;

namespace _2.UsersManagement.Application.Interfaces
{
    public interface IExceptionService
    {
        Task<ExceptionDto> HandleFailure<TDto>(TDto data, Exception ex, string controller, string language, string createdBy);
    }
}
