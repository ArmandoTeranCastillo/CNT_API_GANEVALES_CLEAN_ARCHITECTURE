using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace _2.UsersManagement.Application.DTOs.Distributors.Application.Distributor.in_Controller.RegisterDocuments
{
    public class RegisterDocumentsDto
    {
       public List<InsertDocumentUserDto> Document { get; set; }
    }
}