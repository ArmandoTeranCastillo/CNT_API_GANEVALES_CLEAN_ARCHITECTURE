using System;
using System.IO;
using System.Threading.Tasks;
using _2.UsersManagement.Application.DTOs.Documents;
using _2.UsersManagement.Application.DTOs.Tests;
using _2.UsersManagement.Application.Transients;
using _3.UsersManagement.Infrastructure__EFCore_.External;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UsersManagement.Common.Utilities;

namespace _4.UsersManagement.API.Controllers.Tests
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        //[Authorize]
        private readonly IHandlerUnit _hUnit;
        private readonly IServiceUnit _sUnit;
        private readonly IGenericUnit _gUnit;

        public TestController(IServiceUnit sUnit, IHandlerUnit hUnit, IGenericUnit gUnit)
        {
            _sUnit = sUnit;
            _hUnit = hUnit;
            _gUnit = gUnit;
        }
        
        [HttpPost("EncryptPassword")]
        public IActionResult EncryptPassword(EncryptPasswordDto request)
        {
            var result = Cipher.StringEncrypting(request.Password);
            return Ok(result);
        }
        
        [HttpPost("DecryptPassword")]
        public IActionResult DecryptPassword(DecryptPasswordDto request)
        {
            var result = Cipher.StringDecrypting(request.Password);
            return Ok(result);
        }
        
        [HttpGet("GetPermissions")]
        public async Task<IActionResult> GetPermissions(string id, string language = Value.Language)
        {
            var result = await _sUnit.Auth.GetAllPermissions(id, language);
            return Ok(result);
        }
        
        [HttpPost("TranslateText")]
        public async Task<IActionResult> TranslateText(TranslateTextDto request)
        {
            var result = await _sUnit.Translation.TranslateText(request.Text, request.Language);
            return Ok(result);
        }

        [HttpGet("GetErrorCode")]
        public async Task<IActionResult> GetErrorCode(string code)
        {
            return Ok(await _gUnit.ErrorCode.GetSimpleById(i => i.CodeError == code));
        }
        
        [HttpPost("ConvertToImage")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FileContentResult))]
        public IActionResult ConvertToImage([FromBody] ConvertToImageDto request)
        {
            try
            {
                var formFile = _sUnit.Files.ConvertUriToFormFile(request.Uri);
                byte[] fileBytes;
                using (var ms = new MemoryStream())
                {
                    formFile.CopyTo(ms);
                    fileBytes = ms.ToArray();
                }
                return File(fileBytes, "image/jpeg");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}