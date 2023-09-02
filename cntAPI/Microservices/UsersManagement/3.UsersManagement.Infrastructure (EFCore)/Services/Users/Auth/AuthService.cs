using _2.UsersManagement.Application.Interfaces.Permissions;
using _2.UsersManagement.Application.Interfaces.Users.Auth;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using _2.UsersManagement.Application.DTOs.Permissions.In_Services.GetPermissions;
using _2.UsersManagement.Application.DTOs.Users.Auth.in_Services;
using _2.UsersManagement.Application.DTOs.Users.Consults.in_Services;
using _2.UsersManagement.Application.Interfaces.Users.Consults;
using _2.UsersManagement.Application.Transients;
using _3.UsersManagement.Infrastructure__EFCore_.External;
using _3.UsersManagement.Infrastructure__EFCore_.Persistence;
using _3.UsersManagement.Infrastructure__EFCore_.Services.Users.Auth.Mappings;
using UsersManagement.Common.Errors;
using UsersManagement.Common.Utilities;

namespace _3.UsersManagement.Infrastructure__EFCore_.Services.Users.Auth
{
    public class AuthService : IAuthService
    {
        private readonly CNTContext _cnt;
        private readonly IGenericUnit _gUnit;
        private readonly IUsersService _users;
        private readonly IPermissionsService _permissions;
        private readonly IConfiguration _configuration;

        public AuthService(CNTContext cnt, IGenericUnit gUnit, IUsersService users, IPermissionsService permissions, IConfiguration configuration)
        {
            _cnt = cnt;
            _gUnit = gUnit;
            _users = users;
            _permissions = permissions;
            _configuration = configuration;
        }

        //-----------------------------------------------------PUBLIC-------------------------------------------------------
        public async Task ValidateLogin(string user, string password)
        {
            await _gUnit.User.EntityExists(user, "user");
            await IsRightPassword(user, password);
            //await IsActive(user);
            //await IsNotLogged(user);
            await IsPasswordNotExpired(user);
            await IsRoleAndTypeNotEmpty(user);
        }

        public async Task ValidateLogout(string user)
        {
            await _gUnit.User.EntityExists(user, "user");
            await IsLogged(user);
        }

        public async Task<LoginResponseDto> PrepareLogin(string username, string language)
        {
            var user = await _users.GetSimpleCompleteUserById(i => i.User == username);
            var permissions = await GetAllPermissions(user.User.Id, language);
            await ActivateLogin(user.User);
            var token = CreateToken(user.User);
            await RegisterLoginLog(user.User.Id, 1);
            return AuthMapping.CreateLoginResponse(user, token, permissions);
        }
        
        public async Task<PermissionsDto> GetAllPermissions(string idUser, string language)
        {
            var types = await GetAllUserTypes(idUser);
    
            var permissionsList = new List<PermissionsDto>();
            foreach (var type in types)
            {
                var permissions = await _permissions.GetUserPermissions(2, type, language);
                permissionsList.Add(permissions);
            }
            
            return new PermissionsDto
            {
                Menus = permissionsList.SelectMany(p => p.Menus).Distinct().ToList(),
                MainMenus = permissionsList.SelectMany(p => p.MainMenus).Distinct().ToList(),
                Vistas = permissionsList.SelectMany(p => p.Vistas).Distinct().ToList(),
            };
        }
        private async Task<List<string>> GetAllUserTypes(string idUser)
        {
            return await _cnt.Relations
                .Where(i => i.IdOrigin == idUser && i.IdRelationType == Value.RelationUserType)
                .Select(i => i.IdDestination)
                .ToListAsync();
        }
        
        public async Task PrepareLogout(string id)
        {
            var users = await _gUnit.User.GetSimpleById(i => i.Id == id);
            await DeactivateLogin(users);
            await RegisterLoginLog(users.Id, 2);
        }

        public TokenDto CreateToken(UserDto user)
        {
            try
            {
                var claims = new List<Claim>
                {
                    new(ClaimTypes.Name, user.User),
                };

                var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                    _configuration.GetSection("AppSettings:Token").Value));

                var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

                var expiryDate = DateTime.Now.AddMinutes(5);

                var token = new JwtSecurityToken(
                    claims: claims,
                    expires: expiryDate,
                    signingCredentials: cred
                );

                var jwt = new JwtSecurityTokenHandler().WriteToken(token);

                // Calculating the remaining valid time of the token
                var validFor = expiryDate - DateTime.Now;
                var validForString = $"Valid for {validFor.TotalMinutes:0} minutes";

                return new TokenDto
                {
                    Token = jwt,
                    ExpiresIn = validForString,
                    RememberMe = false
                };
            }
            catch (Exception)
            {
                throw new InternalServerError(Codes.FailedToken);
            }
        }

        //--------------------------------------------PRIVATE------------------------------------------------------

        private async Task IsRightPassword(string user, string password)
        {
            var pass = await _cnt.Users.Where(i => i.User == user).Select(i => i.Password).FirstOrDefaultAsync();
            if (!ValidatePassword(password, pass)) throw new BadRequestException(Codes.WrongPassword);
        }

        private async Task IsRoleAndTypeNotEmpty(string user)
        {
            var roleType = await _cnt.Users
                        .Where(i => i.User == user)
                        .Select(i => new { idUserRole = i.IdUserRole, idUserType = i.IdUserType })
                        .FirstOrDefaultAsync();
            if (string.IsNullOrEmpty(roleType.idUserRole) || string.IsNullOrEmpty(roleType.idUserType))
                throw new NotFoundException(Codes.EmptyField);
        }

        private async Task<bool> IsActive(string user)
        {
            var active = await _cnt.Users.Where(i => i.User == user).Select(i => i.Active).FirstOrDefaultAsync();
            if (!active) throw new BadRequestException(Codes.UserNotActive);
            return true;
        }

        private async Task IsPasswordNotExpired(string user)
        {
            var pass = await _cnt.Users.Where(i => i.User == user).Select(i => i.PasswordExpire).FirstOrDefaultAsync();
            if (pass) throw new BadRequestException(Codes.ExpiredPassword);
        }

        private async Task IsLogged(string user)
        {
            var logged = await _cnt.Users.Where(i => i.User == user).Select(i => i.Logged).FirstOrDefaultAsync();
            if (!logged) throw new BadRequestException(Codes.UserNotLogged);
        }

        private async Task<bool> IsNotLogged(string user)
        {
            var logged = await _cnt.Users.Where(i => i.User == user).Select(i => i.Logged).FirstOrDefaultAsync();
            if (logged) throw new BadRequestException(Codes.UserStillLogged);
            return true;
        }

        private async Task ActivateLogin(UserDto user)
        {
            user.Logged = true;
            await _cnt.SaveChangesAsync();
        }

        private async Task DeactivateLogin(_1.UsersManagement.Domain.Models.Users.Users user)
        {
            user.Logged = false;
            await _cnt.SaveChangesAsync();
        }

        private async Task RegisterLoginLog(string id, int opType)
        {
                await _cnt.Database.ExecuteSqlRawAsync("CNT_SP_USERLOGINLOG @operationType, @idUser",
                     new SqlParameter("@operationType", opType),
                     new SqlParameter("@idUser", id));
        }
        
        private static bool ValidatePassword(string password, string verifyPassword)
        {
            return password == Cipher.StringDecrypting(verifyPassword);
        }
    }
}
