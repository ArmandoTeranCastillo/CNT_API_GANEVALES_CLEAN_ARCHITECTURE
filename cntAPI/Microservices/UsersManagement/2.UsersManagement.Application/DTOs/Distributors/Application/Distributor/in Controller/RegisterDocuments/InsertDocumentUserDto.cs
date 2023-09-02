using Microsoft.AspNetCore.Http;

namespace _2.UsersManagement.Application.DTOs.Distributors.Application.Distributor.in_Controller.RegisterDocuments
{
    public class InsertDocumentUserDto
    {
        public string IdUser { get; set; }
        public string SubIdUser { get; set; } = null;
        public string IdDoctoType { get; set; }
        public string DoctoName { get; set; }
        public string DoctoUri { get; set; }
        public string CreatedBy { get; set; }
    }
}