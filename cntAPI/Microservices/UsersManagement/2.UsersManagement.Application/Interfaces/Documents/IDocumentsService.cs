using System.Collections.Generic;
using System.Threading.Tasks;
using _1.UsersManagement.Domain.Interfaces;
using _1.UsersManagement.Domain.Models.Documents;
using _1.UsersManagement.Domain.Models.Users;
using _2.UsersManagement.Application.DTOs.Distributors.Application.Distributor.in_Controller.RegisterDocuments;
using _2.UsersManagement.Application.DTOs.Documents;
using Microsoft.AspNetCore.Http;

namespace _2.UsersManagement.Application.Interfaces.Documents
{
    public interface IDocumentsService
    {
        Task ValidateRegisterDocumentsDto(RegisterDocumentsDto request);
        void ValidateFileUpload(RegisterDocumentsDto request, List<IFormFile> files);
        Task<List<DocumentDataDto>> GetDocumentInformation(string entity, string subEntity, List<InsertDocumentUserDto> document);
        Task<List<DoctoUsers>> RegisterDocumentUsers(List<DocumentDataDto> information,
            List<InsertDocumentUserDto> requests, List<IFormFile> file);
        Task<List<Profiles>> UpdateDocumentProfile(List<DoctoUsers> document);
        Task<IEnumerable<DoctoTypes>> GetAllCompleteDoctoTypes();
    }
}