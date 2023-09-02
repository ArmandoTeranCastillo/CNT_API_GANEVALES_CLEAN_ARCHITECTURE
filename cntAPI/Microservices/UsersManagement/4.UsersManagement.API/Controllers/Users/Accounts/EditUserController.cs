using _2.UsersManagement.Application.DTOs.Users.Accounts.in_Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using _2.UsersManagement.Application.Transients;
using _3.UsersManagement.Infrastructure__EFCore_.External;
using _3.UsersManagement.Infrastructure__EFCore_.Services.Users.Accounts.Mappings;
using UsersManagement.Common.Errors;
using UsersManagement.Common.Utilities;

namespace _4.UsersManagement.API.Controllers.Users.Accounts
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
        public async Task<IActionResult> ChangePassword(string controllerName, ChangePasswordDto request, string language = Value.Language)
        {
            Http.CleanCache(Response);
            try 
            { 
                switch(controllerName)
                {
                    case "ByAdmin":
                        var user = await _sUnit.EditUser.EditPasswordByAdmin(request);
                        var emailDto = EditMapping.FillSendConfirmationEmailDto(request, user);
                        _ = Task.Run(() => _sUnit.Mail.SendEmail(emailDto));
                        const string message = "Password changed, in a moment you will receive an email";
                        return Ok(await _sUnit.Success.HandleSuccess(message, Codes.OkPut, CNames.ChangePassword, language, request.CreatedBy));

                    default: throw new NotFoundException(Codes.EmptyField);
                }
            }
            catch (Exception ex)
            {
                var error = await _sUnit.Exception.HandleFailure(request, ex, CNames.ChangePassword, language, request.CreatedBy);
                return StatusCode(error.Status, error.Error);
            }
        }
    }
}
