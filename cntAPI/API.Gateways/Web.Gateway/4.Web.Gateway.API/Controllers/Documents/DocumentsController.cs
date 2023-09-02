using System;
using System.Threading.Tasks;
using _2.Web.Gateway.Application.Transients;
using _3.Web.Gateway.Infrastructure__EFCore_.External;
using _3.Web.Gateway.Infrastructure__EFCore_.Services.Urls.Mappings;
using Microsoft.AspNetCore.Mvc;
using Web.Gateway.Common.Utilities;

namespace _4.Web.Gateway.API.Controllers.Documents
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentsController : ControllerBase
    {
        private readonly IServiceUnit _sUnit;
        public DocumentsController(IServiceUnit sUnit)
        {
            _sUnit = sUnit;
        }
        
        [HttpPost("RegisterDocuments")] // Step 1.5
        public async Task<IActionResult> RegisterDocuments(string entity, [FromBody] object json, string subEntity = null)
        {
            Http.CleanCache(Response);
            try
            {
                return Ok(await _sUnit.Request.SendRegisterDocumentsRequest(entity, json, subEntity));
            }
            catch (Exception ex)
            {
                var error = _sUnit.Exception.HandleFailure(ex);
                return StatusCode(error.Status, error.Error);
            }
        }
        
        [HttpGet("GetDocument")]
        public async Task<IActionResult> GetDocument(string id, string userid, string language = Value.Language)
        {
            Http.CleanCache(Response);
            try
            {
                var dto = RequestMappings.FillRequestDto(Hosts.UsersManagement, NUrls.GetDocument, userid, null, null, id, null, language);
                var imageBytes = (byte[])await _sUnit.Request.SendGetRequest(dto);
                return File(imageBytes, "image/jpeg");
            }
            catch (Exception ex)
            {
                var error = _sUnit.Exception.HandleFailure(ex);
                return StatusCode(error.Status, error.Error);
            }
        }
    }
}