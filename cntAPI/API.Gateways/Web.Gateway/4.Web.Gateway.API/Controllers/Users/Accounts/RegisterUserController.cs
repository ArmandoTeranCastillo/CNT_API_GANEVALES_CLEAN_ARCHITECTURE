using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using _2.Web.Gateway.Application.Transients;
using _3.Web.Gateway.Infrastructure__EFCore_.External;
using _3.Web.Gateway.Infrastructure__EFCore_.Services.Urls.Mappings;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging.Abstractions;
using Web.Gateway.Common.Utilities;

namespace _4.Web.Gateway.API.Controllers.Users.Accounts
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterUserController : ControllerBase
    {
        private readonly IServiceUnit _sUnit;

        public RegisterUserController(IServiceUnit sUnit)
        {
            _sUnit = sUnit;
        }

        [HttpPost("RegisterUserAndProfile")]
        public async Task<IActionResult> RegisterUserAndProfile([FromBody] object request)
        {
            Http.CleanCache(Response);
            try
            {
                var dto = RequestMappings.FillRequestDto(Hosts.TestUsersManagement, NUrls.RegisterUserAndProfile, null, null, null, null, request);
                return Ok(await _sUnit.Request.SendPostRequest(dto));
            }
            catch (Exception ex)
            {
                var error = _sUnit.Exception.HandleFailure(ex);
                return StatusCode(error.Status, error.Error);
            }
        }

        [HttpPost("RegisterUserRoleAndUserType")]
        public async Task<IActionResult> RegisterRoleAndType([FromBody] object request)
        {
            Http.CleanCache(Response);
            try
            {
                var dto = RequestMappings.FillRequestDto(Hosts.TestUsersManagement, NUrls.RegisterUserRoleAndUserType, null, null, null, null, request);
                return Ok(await _sUnit.Request.SendPostRequest(dto));
            }
            catch (Exception ex)
            {
                var error = _sUnit.Exception.HandleFailure(ex);
                return StatusCode(error.Status, error.Error);
            }
        }
    }
}
