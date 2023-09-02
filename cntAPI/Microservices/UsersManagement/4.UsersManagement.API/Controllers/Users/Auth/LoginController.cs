using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using _2.UsersManagement.Application.DTOs.Users.Auth.in_Controller;
using _2.UsersManagement.Application.DTOs.Users.Consults.in_Services;
using _2.UsersManagement.Application.Transients;
using _3.UsersManagement.Infrastructure__EFCore_.External;
using UsersManagement.Common.Utilities;

namespace _4.UsersManagement.API.Controllers.Users.Auth
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IGenericUnit _gUnit;
        private readonly IServiceUnit _sUnit;

        public LoginController(IGenericUnit gUnit, IServiceUnit sUnit)
        {
            _gUnit = gUnit;
            _sUnit = sUnit;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto request, string language = Value.Language)
        {
            Http.CleanCache(Response);
            try
            {
                await _sUnit.Auth.ValidateLogin(request.User, request.Password);
                var login = await _sUnit.Auth.PrepareLogin(request.User, language);
                return Ok(await _sUnit.Success.HandleSuccess(login, Codes.OkLogin, CNames.Login, language, "Unknown"));
            }
            catch (Exception ex)
            {
                var error = await _sUnit.Exception.HandleFailure(request, ex, CNames.Login, language, "Unknown");
                return StatusCode(error.Status, error.Error);
            }
        }

        [HttpGet("logout")]
        public async Task<IActionResult> UnLogin(string id, string language = Value.Language)
        {
            Http.CleanCache(Response);
            try
            {
                await _sUnit.Auth.ValidateLogout(id);
                await _sUnit.Auth.PrepareLogout(id);
                return Ok(await _sUnit.Success.HandleSuccess("Logged Out", Codes.OkGet, CNames.Logout, language, "Unknown"));
            }
            catch (Exception ex)
            {
                var error = await _sUnit.Exception.HandleFailure(id, ex, CNames.Logout, language, "Unknown");
                return StatusCode(error.Status, error.Error);
            }
        }

        [HttpPost("GetPermissions")]
        public async Task<IActionResult> GetPermissions(GetPermissionsDto request, string language = Value.Language)
        {
            Http.CleanCache(Response);
            try
            {
                request.Language = _sUnit.Translation.GetIso(request.Language);
                var permissions = await _sUnit.Auth.GetAllPermissions(request.Id, request.Language);
                return Ok(await _sUnit.Success.HandleSuccess(permissions, Codes.OkPost, CNames.GetPermissions, language, request.CreatedBy));
            }
            catch (Exception ex)
            {
                var error = await _sUnit.Exception.HandleFailure("", ex, CNames.GetPermissions, language, request.CreatedBy);
                return StatusCode(error.Status, error.Error);
            }
        }

        [HttpGet("Get-Refresh-Token")]
        public async Task<IActionResult> RefreshToken(string userid, string language = Value.Language)
        {
            Http.CleanCache(Response);
            try 
            {
                var user = await _gUnit.User.GetSimpleById(i => i.Id == userid);
                var userDto = new UserDto
                {
                    Id = user.Id,
                    User = user.User,
                    Email = user.Email,
                    Curp = user.Curp,
                    UserType = user.UserType,
                    UserRole = user.UserRole,
                    Logged = user.Logged
                };
                
                var token = _sUnit.Auth.CreateToken(userDto);
                return Ok(token);
            }
            catch (Exception ex)
            {
                var error = await _sUnit.Exception.HandleFailure("", ex, CNames.RefreshToken, language, "Unknown");
                return StatusCode(error.Status, error.Error);
            }
        }
    }
}
