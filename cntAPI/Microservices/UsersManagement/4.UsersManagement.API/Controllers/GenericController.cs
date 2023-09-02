using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using _1.UsersManagement.Domain.Models.Users;
using _2.UsersManagement.Application.Interfaces;
using _2.UsersManagement.Application.Transients;
using _3.UsersManagement.Infrastructure__EFCore_.External;
using UsersManagement.Common.Errors;
using UsersManagement.Common.Utilities;

namespace _4.UsersManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenericController : ControllerBase
    {
        //[Authorize]
        private readonly IHandlerUnit _hUnit;
        private readonly IServiceUnit _sUnit;
        
        public GenericController(IServiceUnit sUnit, IHandlerUnit hUnit)
        {
            _sUnit = sUnit;
            _hUnit = hUnit;
        }

        [HttpGet("GetAllGeneric")]
        public async Task<IActionResult> GetAll(string controllerName, string entity, string userId, string language = Value.Language)
        {
            Http.CleanCache(Response);
            try
            {
                var result = await Handler(entity);
                return Ok(await _sUnit.Success.HandleSuccess(result, Codes.OkGet, CNames.GetAll, language, userId));
                
                Task<object> Handler(string e) => controllerName.ToLower() switch
                {
                    "users" => _hUnit.Users.HandleGetAll(e),
                    "distributors" => _hUnit.Distributors.HandleGetAll(e),
                    "phones" => _hUnit.Phones.HandleGetAll(e),
                    "documents" => _hUnit.Documents.HandleGetAll(e),
                    "matrix" => _hUnit.Matrix.HandleGetAll(e),
                    _ => throw new NotFoundException(Codes.EmptyField)
                };
            }
            catch (Exception ex)
            {
                var error = await _sUnit.Exception.HandleFailure(controllerName, ex, CNames.GetAll, language, userId);
                return StatusCode(error.Status, error.Error);
            }
        }

        [HttpGet("GetAllGenericById")]
        public async Task<IActionResult> GetAllGenericById(string controllerName, string entity, string reference, string userId, string language = Value.Language)
        {
            Http.CleanCache(Response);
            try
            {
                var result = await Handler(entity, reference);
                return Ok(await _sUnit.Success.HandleSuccess(result, Codes.OkGet, CNames.GetAllById, language, userId));

                Task<object> Handler(string e, string r) =>
                    controllerName.ToLower() switch
                    {
                        "users" => _hUnit.Users.HandleGetAllById(e, r),
                        "addresses" => _hUnit.Addresses.HandleGetAllById(e, r),
                        "distributors" => _hUnit.Distributors.HandleGetAllById(e, r),
                        "tasks" => _hUnit.Tasks.HandleGetAllById(e, r),
                        _ => throw new NotFoundException(Codes.EmptyField)
                    };
            }
            catch (Exception ex)
            {
                var error = await _sUnit.Exception.HandleFailure(controllerName, ex, CNames.GetAllById, language, userId);
                return StatusCode(error.Status, error.Error);
            }
        }

        [HttpGet("GetSimpleGenericById")]
        public async Task<IActionResult> GetSimpleGenericById(string controllerName, string entity, string reference, string userid, string language = Value.Language)
        {
            Http.CleanCache(Response);
            try
            {
                var result = await Handler(entity, reference);
                return Ok(await _sUnit.Success.HandleSuccess(result, Codes.OkGet, CNames.GetSimpleById, language, userid));

                Task<object> Handler(string e, string r) =>
                    controllerName.ToLower() switch
                    {
                        "users" => _hUnit.Users.HandleGetSimpleById(e, r),
                        "addresses" => _hUnit.Addresses.HandleGetSimpleById(e, r),
                        "distributors" => _hUnit.Distributors.HandleGetSimpleById(e, r),
                        "tasks" => _hUnit.Tasks.HandleGetSimpleById(e, r),
                        _ => throw new NotFoundException(Codes.EmptyField)
                    };
            }
            catch (Exception ex)
            {
                var error = await _sUnit.Exception.HandleFailure(controllerName, ex, CNames.GetSimpleById, language, userid);
                return StatusCode(error.Status, error.Error);
            }
        }
        
        [HttpGet("GetAllGenericOneField")]
        public async Task<IActionResult> GetAllOneField(string controllerName, string entity, string userId, string language = Value.Language)
        {
            Http.CleanCache(Response);
            try
            {
                var result = await Handler(entity);
                return Ok(await _sUnit.Success.HandleSuccess(result, Codes.OkGet, CNames.GetAllOneField, language, userId));
                
                Task<object> Handler(string e) => controllerName.ToLower() switch
                {
                    "addresses" => _hUnit.Addresses.HandleGetAllOneField(e),
                    _ => throw new NotFoundException(Codes.EmptyField)
                };
            }
            catch (Exception ex)
            {
                var error = await _sUnit.Exception.HandleFailure(controllerName, ex, CNames.GetAllOneField, language, userId);
                return StatusCode(error.Status, error.Error);
            }
        }

        [HttpGet("GetSimpleGenericOneFieldById")]
        public async Task<IActionResult> GetSimpleGenericOneFieldById(string controllerName, string entity, string reference, string userId, string language = Value.Language)
        {
            Http.CleanCache(Response);
            try
            {
                var result = await Handler(entity, reference);
                return Ok(await _sUnit.Success.HandleSuccess(result, Codes.OkGet, CNames.GetSimpleOneField, language, userId));
                
                Task<object> Handler(string e, string r) =>
                    controllerName.ToLower() switch
                    {
                        "addresses" => _hUnit.Addresses.HandleGetSimpleOneFieldById(e, r),
                        _ => throw new NotFoundException(Codes.EmptyField)
                    };
            }
            catch (Exception ex)
            {
                var error = await _sUnit.Exception.HandleFailure(controllerName, ex, CNames.GetSimpleOneField, language, userId);
                return StatusCode(error.Status, error.Error);
            }
        }

        [HttpPost("InsertGeneric")]
        public async Task<IActionResult> InsertGeneric(string controllerName, string entity, [FromBody] object request, string userid, string language = Value.Language)
        {
            Http.CleanCache(Response);
            try 
            {
                var result = await Handler(entity, request);
                return Ok(await _sUnit.Success.HandleSuccess(result, Codes.OkPost, CNames.Insert, language, userid));

                Task<object> Handler(string e, object r) =>
                    controllerName.ToLower() switch
                    {
                        "users" => _hUnit.Users.HandleInsertGeneric(e, r),
                        "addresses" => _hUnit.Addresses.HandleInsertGeneric(e, r),
                        "distributors" => _hUnit.Distributors.HandleInsertGeneric(e, r),
                        "tasks" => _hUnit.Tasks.HandleInsertGeneric(e, r),
                        _ => throw new NotFoundException(Codes.EmptyField)
                    };
            }
            catch (Exception ex)
            {
                var error = await _sUnit.Exception.HandleFailure(controllerName, ex, CNames.Insert, language, userid);
                return StatusCode(error.Status, error.Error);
            }
        }

        [HttpPut("UpdateGeneric")]
        public async Task<IActionResult> UpdateGeneric(string controllerName, string entity, [FromBody] object request, string userid, string language = Value.Language)
        {
            Http.CleanCache(Response);
            try
            {
                var result = await Handler(entity, request);
                var code = entity is "activateUser" ? Codes.OkUserActivate : Codes.OkPut;
                return Ok(await _sUnit.Success.HandleSuccess(result, code, CNames.Update, language, userid));
                
                Task<object> Handler(string e, object r) =>
                    controllerName.ToLower() switch
                    {
                        "users" => _hUnit.Users.HandleUpdateGeneric(e, r),
                        "addresses" => _hUnit.Addresses.HandleUpdateGeneric(e, r),
                        "distributors" => _hUnit.Distributors.HandleUpdateGeneric(e, r),
                        "tasks" => _hUnit.Tasks.HandleUpdateGeneric(e, r),
                        _ => throw new NotFoundException(Codes.EmptyField)
                    };
            }
            catch (Exception ex)
            {
                var error = await _sUnit.Exception.HandleFailure(controllerName, ex, CNames.Update, language, userid);
                return StatusCode(error.Status, error.Error);
            }
        }
    }
}
