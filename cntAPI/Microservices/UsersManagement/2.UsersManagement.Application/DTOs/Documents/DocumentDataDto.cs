using _1.UsersManagement.Domain.Interfaces;
using _1.UsersManagement.Domain.Models.Users;

namespace _2.UsersManagement.Application.DTOs.Documents
{
    public class DocumentDataDto
    {
        public string Folder { get; set; }
        public string SubFolder { get; set; } = null;
    }
}