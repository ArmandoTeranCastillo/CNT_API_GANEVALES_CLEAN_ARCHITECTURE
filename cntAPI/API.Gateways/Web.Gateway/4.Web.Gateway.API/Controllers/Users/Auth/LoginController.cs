using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using _2.Web.Gateway.Application.Transients;
using _3.Web.Gateway.Infrastructure__EFCore_.External;
using _3.Web.Gateway.Infrastructure__EFCore_.Services.Urls.Mappings;
using Web.Gateway.Common.Utilities;

namespace _4.Web.Gateway.API.Controllers.Users.Auth
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IServiceUnit _sUnit;

        public LoginController(IServiceUnit sUnit)
        {
            _sUnit = sUnit;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] object request, string language = Value.Language)
        {
            Http.CleanCache(Response);
            try
            {
                var dto = RequestMappings.FillRequestDto(Hosts.UsersManagement, NUrls.Login, null, null, null, null, request, language);
                var loginData = await _sUnit.Request.SendPostRequest(dto);
                //SetRefreshToken(loginData.data.token);
                return Ok(loginData);
            }
            catch (Exception ex)
            {
                var error = _sUnit.Exception.HandleFailure(ex);
                return StatusCode(error.Status, error.Error);
            }
        }

        [HttpGet("Logout")]
        public async Task<IActionResult> Logout(string user)
        {
            Http.CleanCache(Response);
            try
            {
                var dto = RequestMappings.FillRequestDto(Hosts.UsersManagement, NUrls.Logout, null, null, null, user);
                var logout = await _sUnit.Request.SendPostRequest(dto);
                return Ok(logout);
            }
            catch (Exception ex)
            {
                var error = _sUnit.Exception.HandleFailure(ex);
                return StatusCode(error.Status, error.Error);
            }
        }
        
        [HttpPost("Refresh-Token")]
        public async Task<IActionResult> RefreshToken(string userid)
        {
            Http.CleanCache(Response);
            try
            {
                var dto = RequestMappings.FillRequestDto(Hosts.UsersManagement, NUrls.RefreshToken, userid);
                var token = await _sUnit.Request.SendGetRequest(dto);
                //Http.SetRefreshToken(token, Response);
                return Ok(token);
            }
            catch (Exception ex)
            {
                var error = _sUnit.Exception.HandleFailure(ex);
                return StatusCode(error.Status, error.Error);
            }
        }
    }
}
