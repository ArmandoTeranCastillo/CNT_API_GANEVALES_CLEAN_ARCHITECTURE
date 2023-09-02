using System;
using _2.Web.Gateway.Application.DTOs.Exceptions;
using _2.Web.Gateway.Application.Interfaces;
using Web.Gateway.Common.Errors;

namespace _3.Web.Gateway.Infrastructure__EFCore_.Services
{
    public class ExceptionService : IExceptionService
    {
        public ExceptionDto HandleFailure(Exception ex)
        {
            return ex switch
            {
                NotFoundException notFoundEx => new ExceptionDto{Status = 404, Error = notFoundEx.json},
                BadRequestException badRequestEx => new ExceptionDto{Status = 400, Error = badRequestEx.json},
                _ => new ExceptionDto{Status = 500, Error = "Internal Server Error"}
            };
        }
    }
}