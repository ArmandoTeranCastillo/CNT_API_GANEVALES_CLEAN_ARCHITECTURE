using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using _2.UsersManagement.Application.Transients;
using _3.UsersManagement.Infrastructure__EFCore_.External;
using Microsoft.AspNetCore.Mvc;
using UsersManagement.Common.Errors;
using UsersManagement.Common.Utilities;

namespace _4.UsersManagement.API.Controllers.Addresses
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class ZipcodesController : ControllerBase
    {
        private readonly IServiceUnit _sUnit;

        public ZipcodesController(IServiceUnit sUnit)
        {
            _sUnit = sUnit;
        }

        [HttpGet("GetSimpleAddressByZipcode")]
        public async Task<IActionResult> GetSimpleAddressByZipcode(string controller, string zipcode, string userId, string language = Value.Language)
        {
            Http.CleanCache(Response);
            try
            {
                Func<string, Task<IEnumerable<string>>> handler = controller.ToLower() switch
                {
                    "states" => _sUnit.Zipcode.GetStateZipcode,
                    "cities" => _sUnit.Zipcode.GetCityZipcode,
                    "countrycode" => _sUnit.Zipcode.GetCountryZipcode,
                    _ => throw new NotFoundException(Codes.EmptyField)
                };
                var result = await handler(zipcode);
                return Ok(await _sUnit.Success.HandleSuccess(result, Codes.OkGet, CNames.SimpleAddressByZipcode, language, userId));
            }
            catch (Exception ex)
            {
                var error = await _sUnit.Exception.HandleFailure(zipcode, ex, CNames.SimpleAddressByZipcode, language, userId);
                return StatusCode(error.Status, error.Error);   
            }
        }
    }
}
