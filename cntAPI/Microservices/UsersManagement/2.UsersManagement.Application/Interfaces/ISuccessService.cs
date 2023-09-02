using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2.UsersManagement.Application.DTOs;

namespace _2.UsersManagement.Application.Interfaces
{
    public interface ISuccessService
    {
        Task<ResponseDto<TDto>> HandleSuccess<TDto>(TDto data, string code, string controller, string language, string createdBy);
    }
}
