using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1.UsersManagement.Domain.Models.Users;
using _2.UsersManagement.Application.DTOs.Permissions.In_Services.GetPermissions;
using _2.UsersManagement.Application.DTOs.Users.Auth.in_Services;
using _2.UsersManagement.Application.DTOs.Users.Consults.in_Services;

namespace _2.UsersManagement.Application.Interfaces.Users.Auth
{
    public interface IAuthService
    {
        Task ValidateLogin(string user, string password);
        Task ValidateLogout(string user);
        Task<LoginResponseDto> PrepareLogin(string username, string language);
        Task<PermissionsDto> GetAllPermissions(string idUser, string language);
        Task PrepareLogout(string id);
        TokenDto CreateToken(UserDto user);
    }
}
