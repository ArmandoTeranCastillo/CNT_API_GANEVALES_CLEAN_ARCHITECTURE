using System;
using _2.Web.Gateway.Application.DTOs.Exceptions;

namespace _2.Web.Gateway.Application.Interfaces
{
    public interface IExceptionService
    {
        ExceptionDto HandleFailure(Exception ex);
    }
}