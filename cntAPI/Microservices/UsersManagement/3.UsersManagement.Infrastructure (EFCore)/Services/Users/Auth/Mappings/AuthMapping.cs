using _2.UsersManagement.Application.DTOs.Permissions.In_Services.GetPermissions;
using _2.UsersManagement.Application.DTOs.Users.Auth.in_Services;

namespace _3.UsersManagement.Infrastructure__EFCore_.Services.Users.Auth.Mappings
{
    public class AuthMapping
    {
        public static LoginResponseDto CreateLoginResponse(GetSimpleUserCompleteDto user, TokenDto token, PermissionsDto permissions)
        {
            return new LoginResponseDto
            {
                Token = token,
                User = user.User,
                Profile = user.Profile,
                Permissions = permissions
            };
        }
    }
}