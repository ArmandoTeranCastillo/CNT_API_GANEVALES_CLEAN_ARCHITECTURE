using _2.UsersManagement.Application.DTOs.Users.Accounts.in_Controllers;
using _2.UsersManagement.Application.DTOs.Users.Accounts.In_Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1.UsersManagement.Domain.Models.Users;

namespace _2.UsersManagement.Application.Interfaces.Users.Accounts
{
    public interface IEditUserService
    {
        Task<_1.UsersManagement.Domain.Models.Users.Users> EditPasswordByAdmin(ChangePasswordDto request);
        Task<_1.UsersManagement.Domain.Models.Users.Users> UpdateUser(UpdateUserDto request);
        Task<Profiles> UpdateProfile(UpdateProfileDto request);
    }
}
