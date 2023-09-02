using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;
using _2.Web.Gateway.Application.Transients;
using _3.Web.Gateway.Infrastructure__EFCore_.External;
using _3.Web.Gateway.Infrastructure__EFCore_.Services.Urls.Mappings;
using Web.Gateway.Common.DTOs;
using Web.Gateway.Common.Errors;
using Web.Gateway.Common.Utilities;

namespace _4.Web.Gateway.API.Controllers.Addresses
{
    [Route("api/[controller]")]
    [ApiController]
    public class ZipcodesController : ControllerBase
    {
        private readonly IServiceUnit _sUnit;

        public ZipcodesController(IServiceUnit sUnit)
        {
            _sUnit = sUnit;
        }

        [HttpGet("GetSimpleAddressByZipcode")]
        public async Task<IActionResult> GetSimpleAddressByZipcode(string controller, string zipcode, string userId)
        {
            Http.CleanCache(Response);
            try
            {
                var dto = RequestMappings.FillRequestDto(Hosts.UsersManagement, NUrls.GetSimpleAddressByZip, userId, controller, null, zipcode);
                var address = await _sUnit.Request.SendGetRequest(dto); 
                return Ok(address);
            }
            catch (Exception ex)
            {
                var error = _sUnit.Exception.HandleFailure(ex);
                return StatusCode(error.Status, error.Error);
            }
        }
    }
}
