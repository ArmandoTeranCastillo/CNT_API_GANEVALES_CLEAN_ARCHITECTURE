using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using _2.UsersManagement.Application.DTOs.Users.Accounts.in_Controllers;
using _2.UsersManagement.Application.Transients;
using _3.UsersManagement.Infrastructure__EFCore_.External;
using UsersManagement.Common.Utilities;

namespace _4.UsersManagement.API.Controllers.Users.Accounts
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
        public async Task<IActionResult> RegisterUserAndProfile(RegisterUserAndProfileDto request, string language = Value.Language)
        {
            Http.CleanCache(Response);
            try 
            {
                _sUnit.RegisterUser.ValidateRegisterUserAndProfileDto(request);
                var inputRegister = await _sUnit.RegisterUser.PrepareRegisterUserAndProfile(request);
                if (!string.IsNullOrEmpty(inputRegister.ValidationResult.IdUser))
                    return Ok(await _sUnit.Success.HandleSuccess(inputRegister.ValidationResult.IdUser, Codes.OkPost,
                        CNames.RegisterUserAndProfile, language, request.CreatedBy));
                var result = await _sUnit.RegisterUser.InsertUserAndProfile(request, inputRegister);
                return Ok(await _sUnit.Success.HandleSuccess(result.User.Id, Codes.OkPost, CNames.RegisterUserAndProfile, language, request.CreatedBy));
            }
            catch (Exception ex)
            {
                var error = await _sUnit.Exception.HandleFailure(request, ex, CNames.RegisterUserAndProfile, language, request.CreatedBy);
                return StatusCode(error.Status, error.Error);
            }
        }

        [HttpPost("RegisterUserRoleAndUserType")]
        public async Task<IActionResult> RegisterRoleAndType(RegisterRoleAndTypeDto request, string language = Value.Language)
        {
            Http.CleanCache(Response);
            try 
            {
                string insertRelation = null;
                if (!string.IsNullOrEmpty(request.IdCredinet))
                {
                    try
                    {
                        await _sUnit.RegisterUser.InsertUserRelation(request);
                    }
                    catch (Exception)
                    {
                        insertRelation = $"Error trying to link old account";
                    }
                }

                var result = await _sUnit.RegisterUser.UpdateRoleAndType(request);

                await _sUnit.RegisterUser.GeneratePermissions(request);
                _ = _sUnit.RegisterUser.SendConfirmationEmail(request);

                return base.Ok(await _sUnit.Success.HandleSuccess(new { result, insertRelation }, Codes.OkPost, CNames.RegisterRoleAndType, language, request.CreatedBy));
            }
            catch (Exception ex)
            {
                var error = await _sUnit.Exception.HandleFailure(request, ex, CNames.RegisterRoleAndType, language, request.CreatedBy);
                return StatusCode(error.Status, error.Error);
            }
        }
    }
}
