using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;
using _2.Web.Gateway.Application.Transients;
using _3.Web.Gateway.Infrastructure__EFCore_.External;
using _3.Web.Gateway.Infrastructure__EFCore_.Services.Urls.Mappings;
using Web.Gateway.Common.Errors;
using Web.Gateway.Common.Utilities;

namespace _4.Web.Gateway.API.Controllers.Users.Accounts
{
    [Route("api/[controller]")]
    [ApiController]
    public class EditUserController : ControllerBase
    {
        private readonly IServiceUnit _sUnit;

        public EditUserController(IServiceUnit sUnit)
        {
            _sUnit = sUnit;
        }

        [HttpPut("ChangePassword")]
        public async Task<IActionResult> ChangePassword(string controllerName, [FromBody] object request)
        {
            Http.CleanCache(Response);
            try
            {
                var dto = RequestMappings.FillRequestDto(Hosts.UsersManagement, NUrls.ChangePassword, null, controllerName, null, null, request);
                return controllerName switch
                {
                    "ByAdmin" => Ok(await _sUnit.Request.SendPutRequest(dto)),
                    _ => throw new NotFoundException(HttpStatusCode.NotFound, Codes.EmptyField, "Empty Field")
                };
            }
            catch (Exception ex)
            {
                var error = _sUnit.Exception.HandleFailure(ex);
                return StatusCode(error.Status, error.Error);
            }
        }
    }
}
