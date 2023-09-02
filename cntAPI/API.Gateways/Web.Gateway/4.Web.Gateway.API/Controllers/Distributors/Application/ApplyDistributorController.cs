using System;
using System.Threading.Tasks;
using _2.Web.Gateway.Application.Transients;
using _3.Web.Gateway.Infrastructure__EFCore_.External;
using _3.Web.Gateway.Infrastructure__EFCore_.Services.Urls.Mappings;
using Microsoft.AspNetCore.Mvc;
using Web.Gateway.Common.Utilities;

namespace _4.Web.Gateway.API.Controllers.Distributors.Application
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplyDistributorController : ControllerBase
    {
        private readonly IServiceUnit _sUnit;

        public ApplyDistributorController(IServiceUnit sUnit)
        {
            _sUnit = sUnit;
        }

        [HttpPost("RegisterProspect")] // ---- Step 0  
        public async Task<IActionResult> RegisterProspect([FromBody] object request)
        {
            Http.CleanCache(Response);
            try
            {
                var dto = RequestMappings.FillRequestDto(Hosts.UsersManagement, NUrls.RegisterProspect, null, null, null, null, request);
                return Ok(await _sUnit.Request.SendPostRequest(dto));
            }
            catch (Exception ex)
            {
                var error = _sUnit.Exception.HandleFailure(ex);
                return StatusCode(error.Status, error.Error);
            }
        }

        [HttpPost("AssignAppointment")] // ---- Step 0.5
        public async Task<IActionResult> AssignAppointment([FromBody] object request)
        {
            Http.CleanCache(Response);
            try
            {
                var dto = RequestMappings.FillRequestDto(Hosts.UsersManagement, NUrls.AssignAppointment, null, null, null, null, request);
                return Ok(await _sUnit.Request.SendPostRequest(dto));
            }
            catch (Exception ex)
            {
                var error = _sUnit.Exception.HandleFailure(ex);
                return StatusCode(error.Status, error.Error);
            }
        }
        
        [HttpPost("RegisterDistributor")] // ---- Step 1
        public async Task<IActionResult> RegisterDistributor([FromBody] object request)
        {
            Http.CleanCache(Response);
            try
            {
                var dto = RequestMappings.FillRequestDto(Hosts.UsersManagement, NUrls.RegisterDistributor, null, null, null, null, request);
                return Ok(await _sUnit.Request.SendPostRequest(dto));
            }
            catch (Exception ex)
            {
               var error = _sUnit.Exception.HandleFailure(ex);
               return StatusCode(error.Status, error.Error);
            }
        }

        [HttpPost("RegisterEndorsement")] // ---- Step 1
        public async Task<IActionResult> RegisterEndorsement([FromBody] object request)
        {
            Http.CleanCache(Response);
            try
            {
                var dto = RequestMappings.FillRequestDto(Hosts.UsersManagement, NUrls.RegisterEndorsement, null, null, null, null, request);
                return Ok(await _sUnit.Request.SendPostRequest(dto));
            }
            catch (Exception ex)
            {
                var error =_sUnit.Exception.HandleFailure(ex);
                return StatusCode(error.Status, error.Error);
            }
        }
        
        [HttpPost("RegisterDocuments")] // Step 1.5
        public async Task<IActionResult> RegisterDocuments([FromBody] object request)
        {
            Http.CleanCache(Response);
            try
            {
                var dto = RequestMappings.FillRequestDto(Hosts.UsersManagement, NUrls.RegisterDocuments, null, null, null, null, request);
                return Ok(await _sUnit.Request.SendPostRequest(dto));
            }
            catch (Exception ex)
            {
                var error = _sUnit.Exception.HandleFailure(ex);
                return StatusCode(error.Status, error.Error);
            }
        }

        [HttpPost("RegisterJobInfo")] // ---- Step 2
        public async Task<IActionResult> RegisterJobInfo([FromBody] object request)
        {
            Http.CleanCache(Response);
            try
            {
                var dto = RequestMappings.FillRequestDto(Hosts.UsersManagement, NUrls.RegisterJobInfo, null, null, null, null, request);
                return Ok(await _sUnit.Request.SendPostRequest(dto));
            }
            catch (Exception ex)
            {
                var error = _sUnit.Exception.HandleFailure(ex);
                return StatusCode(error.Status, error.Error);
            }
        }
        
        [HttpPost("RegisterSalesXp")] // ---- Step 3 -- Only for Distributors
        public async Task<IActionResult> RegisterSalesXp([FromBody] object request)
        {
            Http.CleanCache(Response);
            try
            {
                var dto = RequestMappings.FillRequestDto(Hosts.UsersManagement, NUrls.RegisterSalesXp, null, null, null, null, request);
                return Ok(await _sUnit.Request.SendPostRequest(dto));
            }
            catch (Exception ex)
            {
                var error = _sUnit.Exception.HandleFailure(ex);
                return StatusCode(error.Status, error.Error);
            }
        }

        [HttpPost("RegisterSpouse")] // ---- Step 4
        public async Task<IActionResult> RegisterSpouse([FromBody] object request)
        {
            Http.CleanCache(Response);
            try
            {
                var dto = RequestMappings.FillRequestDto(Hosts.UsersManagement, NUrls.RegisterSpouse, null, null, null, null, request);
                return Ok(await _sUnit.Request.SendPostRequest(dto));
            }
            catch (Exception ex)
            {
                var error = _sUnit.Exception.HandleFailure(ex);
                return StatusCode(error.Status, error.Error);
            }
        }
        
        [HttpPost("RegisterSpouseJobInfo")] // ---- Step 5
        public async Task<IActionResult> RegisterSpouseJobInfo([FromBody] object request)
        {
            Http.CleanCache(Response);
            try
            {
                var dto = RequestMappings.FillRequestDto(Hosts.UsersManagement, NUrls.RegisterSpouseJobInfo, null, null, null, null, request);
                return Ok(await _sUnit.Request.SendPostRequest(dto));
            }
            catch (Exception ex)
            {
                var error = _sUnit.Exception.HandleFailure(ex);
                return StatusCode(error.Status, error.Error);
            }
        }

        [HttpPut("UpdateTotalIncoming")] // ---- Step 5.5 
        public async Task<IActionResult> UpdateTotalIncoming(string entity, string id, string userid)
        {
            Http.CleanCache(Response);
            try
            {
                var dto = RequestMappings.FillRequestDto(Hosts.UsersManagement, NUrls.UpdateTotalIncoming, userid, null, entity, id);
                return Ok(await _sUnit.Request.SendPutRequest(dto));
            }
            catch (Exception ex)
            {
               var error = _sUnit.Exception.HandleFailure(ex);
               return StatusCode(error.Status, error.Error);
            }
        }

        [HttpPost("RegisterDependents")] // ---- Step 6
        public async Task<IActionResult> RegisterDependents([FromBody] object request)
        {
            Http.CleanCache(Response);
            try
            {
                var dto = RequestMappings.FillRequestDto(Hosts.UsersManagement, NUrls.RegisterDependents, null, null, null, null, request);
                return Ok(await _sUnit.Request.SendPostRequest(dto));
            }
            catch (Exception ex)
            {
                var error = _sUnit.Exception.HandleFailure(ex);
                return StatusCode(error.Status, error.Error);
            }
        }

        [HttpPost("RegisterVehicles")] // ---- Step 7
        public async Task<IActionResult> RegisterVehicles([FromBody] object request)
        {
            Http.CleanCache(Response);
            try
            {
                var dto = RequestMappings.FillRequestDto(Hosts.UsersManagement, NUrls.RegisterVehicles, null, null, null, null, request);
                return Ok(await _sUnit.Request.SendPostRequest(dto));
            }
            catch (Exception ex)
            {
                var error = _sUnit.Exception.HandleFailure(ex);
                return StatusCode(error.Status, error.Error);
            }
        }

        [HttpPost("RegisterReferences")] // ---- Step 8
        public async Task<IActionResult> RegisterReferences([FromBody] object request)
        {
            Http.CleanCache(Response);
            try
            {
                var dto = RequestMappings.FillRequestDto(Hosts.UsersManagement, NUrls.RegisterReferences, null, null, null, null, request);
                return Ok(await _sUnit.Request.SendPostRequest(dto));
            }
            catch (Exception ex)
            {
                var error = _sUnit.Exception.HandleFailure(ex);
                return StatusCode(error.Status, error.Error);
            }
        }

        [HttpPost("ValidateCosts")] // ---- Step 9
        public async Task<IActionResult> ValidateCosts(string entity, string id, string userid)
        {
            Http.CleanCache(Response);
            try
            {
                var dto = RequestMappings.FillRequestDto(Hosts.UsersManagement, NUrls.ValidateCosts, userid, null, entity, id);
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