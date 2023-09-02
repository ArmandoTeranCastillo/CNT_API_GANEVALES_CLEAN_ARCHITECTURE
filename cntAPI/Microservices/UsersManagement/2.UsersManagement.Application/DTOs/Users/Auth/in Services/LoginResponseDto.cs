using _1.UsersManagement.Domain.Models.Users;
using _2.UsersManagement.Application.DTOs.Permissions.In_Services.GetPermissions;
using _2.UsersManagement.Application.DTOs.Users.Consults.in_Services;

namespace _2.UsersManagement.Application.DTOs.Users.Auth.in_Services
{
    public class LoginResponseDto
    {
        public TokenDto Token { get; set; } 
        public UserDto User { get; set; }
        public Profiles Profile { get; set; }
        public PermissionsDto Permissions { get; set; }
    }
}
