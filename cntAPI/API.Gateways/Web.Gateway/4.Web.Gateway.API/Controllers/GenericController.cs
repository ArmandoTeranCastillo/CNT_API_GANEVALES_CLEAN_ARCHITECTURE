using System;
using System.Net;
using System.Threading.Tasks;
using _2.Web.Gateway.Application.Transients;
using _3.Web.Gateway.Infrastructure__EFCore_.External;
using Microsoft.AspNetCore.Mvc;
using Web.Gateway.Common.Errors;
using Web.Gateway.Common.Utilities;

namespace _4.Web.Gateway.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenericController : ControllerBase
    {
        private readonly IServiceUnit _sUnit;
        private readonly IHandlerUnit _hUnit;

        public GenericController(IServiceUnit sUnit, IHandlerUnit hUnit)
        {
            _sUnit = sUnit;
            _hUnit = hUnit;
        }

        [HttpGet("GetAllGenericOneField")]
        public async Task<IActionResult> GetAll(string controllerName, string entity,  string userId)
        {
            Http.CleanCache(Response);
            try
            {
                return controllerName.ToLower() switch
                {
                    "addresses" => Ok(await _hUnit.Addresses.HandleGetAllOneField(controllerName, entity, userId)),
                    _ => throw new NotFoundException(HttpStatusCode.NotFound, Codes.EmptyField, "Empty Field")
                };
            }
            catch (Exception ex)
            {
                var error = _sUnit.Exception.HandleFailure(ex);
                return StatusCode(error.Status, error.Error);
            }
        }

        [HttpGet("GetSimpleGenericOneFieldById")]
        public async Task<IActionResult> GetSimpleGenericOneFieldById(string controllerName, string entity, string reference, string userid)
        {
            Http.CleanCache(Response);
            try
            {
                return controllerName.ToLower() switch
                {
                    "addresses" => Ok(await _hUnit.Addresses.HandleGetSimpleOneFieldById(controllerName, entity, reference, userid)),
                    _ => throw new NotFoundException(HttpStatusCode.NotFound, Codes.EmptyField, "Empty Field")
                };
            }
            catch (Exception ex)
            {
               var error = _sUnit.Exception.HandleFailure(ex);
               return StatusCode(error.Status, error.Error);
            }
        }

        [HttpGet("GetAllGeneric")]
        public async Task<IActionResult> GetAllGeneric(string controllerName, string entity, string userId)
        {
            Http.CleanCache(Response);
            try
            {
                return controllerName.ToLower() switch
                {
                    "users" => Ok(await _hUnit.Users.HandleGetAll(controllerName, entity, userId)),
                    "distributors" => Ok(await _hUnit.Distributors.HandleGetAll(controllerName, entity, userId)),
                    "phones" => Ok(await _hUnit.Phones.HandleGetAll(controllerName, entity, userId)),
                    "matrix" => Ok(await _hUnit.Matrix.HandleGetAll(controllerName, entity, userId)),
                    _ => throw new NotFoundException(HttpStatusCode.NotFound, Codes.EmptyField, "Empty Field")
                };
            }
            catch (Exception ex)
            {
                var error = _sUnit.Exception.HandleFailure(ex);
                return StatusCode(error.Status, error.Error);
            }
        }


        [HttpGet("GetAllGenericById")]
        public async Task<IActionResult> GetAllGenericById(string controllerName, string entity, string reference, string userId)
        {
            Http.CleanCache(Response);
            try
            {
                return controllerName.ToLower() switch
                {
                    "users" => Ok(await _hUnit.Users.HandleGetAllById(controllerName, entity, reference, userId)),
                    "addresses" => Ok(await _hUnit.Addresses.HandleGetAllById(controllerName, entity, reference, userId)),
                    "distributors" => Ok(await _hUnit.Distributors.HandleGetAllById(controllerName, entity, reference, userId)),
                    "tasks" => Ok(await _hUnit.Tasks.HandleGetAllById(controllerName, entity, reference, userId)),
                    _ => throw new NotFoundException(HttpStatusCode.NotFound, Codes.EmptyField, "Empty Field")
                };
            }
            catch (Exception ex)
            {
                var error = _sUnit.Exception.HandleFailure(ex);
                return StatusCode(error.Status, error.Error);
            }
        }

        [HttpGet("GetSimpleGenericById")]
        public async Task<IActionResult> GetSimpleGenericById(string controllerName, string entity, string reference, string userid)
        {
            Http.CleanCache(Response);
            try
            {
                return controllerName.ToLower() switch
                {
                    "users" => Ok(await _hUnit.Users.HandleGetSimpleById(controllerName, entity, reference, userid)),
                    "distributors" => Ok(await _hUnit.Distributors.HandleGetSimpleById(controllerName, entity, reference, userid)),
                    "addresses" => Ok(await _hUnit.Addresses.HandleGetSimpleById(controllerName, entity, reference, userid)),
                    "tasks" => Ok(await _hUnit.Tasks.HandleGetSimpleById(controllerName, entity, reference, userid)),
                    _ => throw new NotFoundException(HttpStatusCode.NotFound, Codes.EmptyField, "Empty Field")
                };
            }
            catch (Exception ex)
            {
                var error = _sUnit.Exception.HandleFailure(ex);
                return StatusCode(error.Status, error.Error);
            }
        }

        [HttpPost("InsertGeneric")]
        public async Task<IActionResult> InsertGeneric(string controllerName, string entity, [FromBody] object request, string userid)
        {
            Http.CleanCache(Response);
            try
            {
                return controllerName.ToLower() switch
                {
                    "addresses" => Ok(await _hUnit.Addresses.HandleInsertGeneric(controllerName, entity, request, userid)),
                    "tasks" => Ok(await _hUnit.Tasks.HandleInsertGeneric(controllerName, entity, request, userid)),
                    _ => throw new NotFoundException(HttpStatusCode.NotFound, Codes.EmptyField, "Empty Field")
                };
            }
            catch (Exception ex)
            {
                var error = _sUnit.Exception.HandleFailure(ex);
                return StatusCode(error.Status, error.Error);
            }
        }

        [HttpPut("UpdateGeneric")]
        public async Task<IActionResult> UpdateGeneric(string controllerName, string entity, [FromBody] object request, string userid)
        {
            Http.CleanCache(Response);
            try
            {
                return controllerName.ToLower() switch
                {
                    "users" => Ok(await _hUnit.Users.HandleUpdateGeneric(controllerName, entity, request, userid)),
                    "addresses" => Ok(await _hUnit.Addresses.HandleUpdateGeneric(controllerName, entity, request, userid)),
                    "tasks" => Ok(await _hUnit.Tasks.HandleUpdateGeneric(controllerName, entity, request, userid)),
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

