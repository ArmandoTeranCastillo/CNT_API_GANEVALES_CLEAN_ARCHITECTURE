using UsersManagement.Common.Utilities;

namespace _2.UsersManagement.Application.DTOs.Documents
{
    public class GetDocumentDto
    {
        public string id { get; set; }
        public string userid { get; set; }
        public string language { get; set; } = Value.Language;
    }
}