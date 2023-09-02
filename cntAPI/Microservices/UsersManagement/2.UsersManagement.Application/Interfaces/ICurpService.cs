using _2.UsersManagement.Application.DTOs.Users.Accounts.In_Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2.UsersManagement.Application.DTOs.Users.Accounts.in_Controllers;

namespace _2.UsersManagement.Application.Interfaces
{
    public interface ICurpService 
    {
        string CreateCurp(GenerateCurpDto request);
        Task<int> GetStateEnum(string idBirthState);
    }
}
