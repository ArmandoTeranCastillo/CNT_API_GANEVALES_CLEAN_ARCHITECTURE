using Microsoft.AspNetCore.Http;

namespace _2.UsersManagement.Application.Interfaces.Documents
{
    public interface IFilesService
    {
        byte[] ConvertUriToImage(string dataUri);
        IFormFile ConvertUriToFormFile(string dataUri);
    }
}