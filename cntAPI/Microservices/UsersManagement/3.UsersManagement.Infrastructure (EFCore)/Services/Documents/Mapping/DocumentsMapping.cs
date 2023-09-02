using System;
using _1.UsersManagement.Domain.Models.Documents;
using _2.UsersManagement.Application.DTOs.Distributors.Application.Distributor.in_Controller.RegisterDocuments;

namespace _3.UsersManagement.Infrastructure__EFCore_.Services.Documents.Mapping
{
    public abstract class DocumentsMapping
    {
        public static DoctoUsers FillModelDoctoUser(InsertDocumentUserDto document)
        {
            var id = document.SubIdUser ?? document.IdUser;
            return new DoctoUsers
            {
                Id = Guid.NewGuid().ToString().ToUpper(),
                IdDoctoType = document.IdDoctoType,
                IdUser = id,
                DoctoName = document.DoctoName,
                DoctRoute = string.Empty,
                DocUrls = string.Empty,
                Active = true,
                CreatedAt = DateTime.Now,
                CreatedBy = document.CreatedBy,
                ModiffiedAt = DateTime.Now,
                ModiffiedBy = document.CreatedBy
            };
        }
    }
}