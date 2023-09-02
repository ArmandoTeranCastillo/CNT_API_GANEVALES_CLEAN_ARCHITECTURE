using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using _1.UsersManagement.Domain.Interfaces;
using _1.UsersManagement.Domain.Models.Distributors;
using _1.UsersManagement.Domain.Models.Documents;
using _1.UsersManagement.Domain.Models.Users;
using _2.UsersManagement.Application.DTOs.Distributors.Application.Distributor.in_Controller.RegisterDocuments;
using _2.UsersManagement.Application.DTOs.Documents;
using _2.UsersManagement.Application.Handlers;
using _2.UsersManagement.Application.Interfaces.Documents;
using _2.UsersManagement.Application.Transients;
using _3.UsersManagement.Infrastructure__EFCore_.External;
using _3.UsersManagement.Infrastructure__EFCore_.Persistence;
using _3.UsersManagement.Infrastructure__EFCore_.Services.Documents.Mapping;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using UsersManagement.Common.Errors;
using UsersManagement.Common.Utilities;

namespace _3.UsersManagement.Infrastructure__EFCore_.Services.Documents
{
    public class DocumentsService : IDocumentsService
    {
        private readonly IGenericUnit _gUnit;
        private readonly CNTContext _cnt;
        public DocumentsService(CNTContext cnt, IGenericUnit gUnit)
        {
            _gUnit = gUnit;
            _cnt = cnt;
        }
        
        public async Task ValidateRegisterDocumentsDto(RegisterDocumentsDto request)
        {
            foreach (var document in request.Document)
            {
                await ValidateInsertDocumentUserDto(document);
            }
        }

        public void ValidateFileUpload(RegisterDocumentsDto request, List<IFormFile> files)
        {
            if (files == null || !files.Any())
            {
                throw new BadRequestException("File list is empty");
            }
            if (request.Document.Count != files.Count)
            {
                throw new BadRequestException("The number of files does not match the number of documents");
            }
            if (files.Any(file => file == null || file.Length == 0))
            {
                throw new BadRequestException("One of the files is empty");
            }
        }
        
        public async Task<List<DocumentDataDto>> GetDocumentInformation(string entity, string subEntity, List<InsertDocumentUserDto> documents)
        {
            return (await Task.WhenAll(
                documents.Select(async document => new DocumentDataDto
                {
                    Folder = GetTypeUser(entity),
                    SubFolder = document.SubIdUser == null ? null : GetTypeUser(subEntity),
                })
            )).ToList();
        }
        
        private static string GetTypeUser(string entity)
        {
            return entity switch
            {
                "Distributor" => "Distributors",
                "Aval" => "Avals",
                "Profiles" => "Profiles",
                _ => throw new BadRequestException(Codes.EmptyField)
            };
        }
        
        public async Task<List<DoctoUsers>> RegisterDocumentUsers(List<DocumentDataDto> informations, List<InsertDocumentUserDto> requests, List<IFormFile> files)
        {
            var tasks = new List<Task<DoctoUsers>>();
    
            for (var i = 0; i < informations.Count && i < requests.Count; i++)
            {
                var information = informations[i];
                var request = requests[i];
                var file = files[i];
        
                tasks.Add(RegisterDocumentUser(information, request, file));
            }
            var results = await Task.WhenAll(tasks);
            return results.ToList();
        }
        
        private async Task<DoctoUsers> RegisterDocumentUser(DocumentDataDto information, InsertDocumentUserDto request, IFormFile file)
        {
            var documentUser = DocumentsMapping.FillModelDoctoUser(request);
            var path = await GenerateRouteAndSaveFile(information, request, file, Value.LocalPath);
            var url = await GenerateUrl(information, request, file, Value.HostPath);
            documentUser.DoctRoute = path;
            documentUser.DocUrls = url;
            documentUser.DoctRoute = Cipher.StringEncrypting(documentUser.DoctRoute);
            documentUser.DocUrls = Cipher.StringEncrypting(documentUser.DocUrls);
            documentUser.DoctoName = Cipher.StringEncrypting(documentUser.DoctoName);
            _cnt.DoctoUsers.Add(documentUser);
            await _cnt.SaveChangesAsync();
            return documentUser;
        }
        
        private async Task<string> GenerateRouteAndSaveFile(DocumentDataDto information, InsertDocumentUserDto document, IFormFile file, string basePath)
        {
            var path = await GenerateRoute(information, document, file, basePath);
            await SaveFile(file, path);
            return path;
        }

        private async Task<string> GenerateRoute(DocumentDataDto information, InsertDocumentUserDto document, IFormFile file, string basePath)
        {
            var doctype = await _gUnit.DoctoType.GetEntityProperty(document.IdDoctoType, "id", "DoctoType");
            var extension = Path.GetExtension(file.FileName);
            var pathGenerator = new PathGenerator(basePath);
            
            return pathGenerator
                .GeneratePath(information.Folder, document.IdUser, doctype, document.DoctoName, extension,
                    information.SubFolder, document.SubIdUser);
        }
        
        private async Task<string> GenerateUrl(DocumentDataDto information, InsertDocumentUserDto document, IFormFile file, string basePath)
        {
            var doctype = await _gUnit.DoctoType.GetEntityProperty(document.IdDoctoType, "id", "DoctoType");
            var extension = Path.GetExtension(file.FileName);
            var pathGenerator = new PathGenerator(basePath);
            
            return pathGenerator
                .GenerateUrl(information.Folder, document.IdUser, doctype, document.DoctoName, extension,
                    information.SubFolder, document.SubIdUser);
        }

        private async Task SaveFile(IFormFile file, string path)
        {
            await using var fileStream = new FileStream(path, FileMode.Create);
            await file.CopyToAsync(fileStream);
        }
        
        public async Task<List<Profiles>> UpdateDocumentProfile(List<DoctoUsers> document)
        {
            var updatedProfiles = new List<Profiles>();
            foreach (var docUser in document)
            {
                var profile = await _gUnit.Profile.GetSimpleById(i => i.IdUser == docUser.IdUser);
                if (profile == null) continue;
                profile.IdDocNumber = docUser.Id;
                profile.IdDocType = docUser.IdDoctoType;
                updatedProfiles.Add(profile);
            }
            await _cnt.SaveChangesAsync();
            return updatedProfiles;
        }
        
        public async Task<IEnumerable<DoctoTypes>> GetAllCompleteDoctoTypes()
        {
            return await _cnt.DoctoTypes
                .Include(i => i.Matrices)
                .ToListAsync();
        }
        
        private async Task ValidateInsertDocumentUserDto(InsertDocumentUserDto request)
        {
            await _gUnit.User.EntityExists(request.IdUser, "id");
            try
            {
                await _gUnit.User.EntityExists(request.SubIdUser, "id");
            }
            catch (Exception)
            {
                request.SubIdUser = null;
            }
            //await _gUnit.DoctoUser.EntityPropertyHasNot(request.IdDoctoType, "IdDoctoType", request.IdUser, "IdUser");
            await _gUnit.DoctoType.EntityExists(request.IdDoctoType, "id");
            await _gUnit.User.EntityExists(request.CreatedBy, "id");
        }
    }
}