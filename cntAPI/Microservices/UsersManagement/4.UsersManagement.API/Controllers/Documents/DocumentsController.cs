using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using _2.UsersManagement.Application.DTOs.Distributors.Application.Distributor.in_Controller.RegisterDocuments;
using _2.UsersManagement.Application.DTOs.Documents;
using _2.UsersManagement.Application.Transients;
using _3.UsersManagement.Infrastructure__EFCore_.External;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using UsersManagement.Common.Errors;
using UsersManagement.Common.Utilities;

namespace _4.UsersManagement.API.Controllers.Documents
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentsController : ControllerBase
    {
        private readonly IServiceUnit _sUnit;
        private readonly IGenericUnit _gUnit;
        public DocumentsController(IServiceUnit sUnit, IGenericUnit gUnit)
        {
            _sUnit = sUnit;
            _gUnit = gUnit;
        }
        
        [HttpPost("RegisterDocuments")] // Step 1.5
        public async Task<IActionResult> RegisterDocuments(string entity, RegisterDocumentsDto requests, string subEntity = null, string language = Value.Language)
        {
            Http.CleanCache(Response);
            try
            {
                await _sUnit.Documents.ValidateRegisterDocumentsDto(requests);
                var files = requests.Document.Select(document => _sUnit.Files.ConvertUriToFormFile(document.DoctoUri)).ToList();
                _sUnit.Documents.ValidateFileUpload(requests, files);
                
                var information = await _sUnit.Documents.GetDocumentInformation(entity, subEntity, requests.Document);
                var doctoUser = await _sUnit.Documents.RegisterDocumentUsers(information, requests.Document, files);
                //await _sUnit.Documents.UpdateDocumentProfile(doctoUser);
                var result = doctoUser.Select(s => new { idDocument = s.Id }).ToList();
                return Ok(await _sUnit.Success.HandleSuccess(result, Codes.OkPost, CNames.RegisterDocuments, language, requests.Document[0].CreatedBy));
            }
            catch (Exception ex)
            {
                var error = await _sUnit.Exception.HandleFailure(requests, ex, CNames.RegisterDocuments, language, requests.Document[0].CreatedBy);
                return StatusCode(error.Status, error.Error);
            }
        }
        
        [HttpGet("GetDocument")]
        public async Task<IActionResult> GetDocument(string id, string userid, string language = Value.Language)
        {
            Http.CleanCache(Response);
            try
            {
                using var httpClient = new HttpClient();
                var document = await _gUnit.DoctoUser.GetSimpleById(i => i.Id == id);
                var url = Cipher.StringDecrypting(document.DocUrls);
                var imageBytes = await httpClient.GetByteArrayAsync(url);
                return File(imageBytes, "image/jpeg");
            }
            catch (Exception ex)
            {
                var error = await _sUnit.Exception.HandleFailure(id, ex, CNames.GetDocument, language, userid);
                return StatusCode(error.Status, error.Error);
            }
        }
    }
}