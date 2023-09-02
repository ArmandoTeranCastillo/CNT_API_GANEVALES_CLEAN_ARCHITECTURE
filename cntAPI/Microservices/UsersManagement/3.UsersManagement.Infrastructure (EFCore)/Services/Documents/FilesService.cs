using System;
using System.IO;
using _2.UsersManagement.Application.Interfaces.Documents;
using _3.UsersManagement.Infrastructure__EFCore_.External;
using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;

namespace _3.UsersManagement.Infrastructure__EFCore_.Services.Documents
{
    public class FilesService : IFilesService
    {
        public byte[] ConvertUriToImage(string dataUri)
        {
            try
            {
                var base64Data = dataUri.Split(',')[1];
                var imageBytes = Convert.FromBase64String(base64Data);
                using var ms = new MemoryStream(imageBytes);
                using var image = Image.Load(ms);
                using var output = new MemoryStream();
                image.Save(output, new JpegEncoder());
                return output.ToArray();
            }
            catch (Exception ex)
            {
                throw new Exception($"No se pudo convertir el Data URI a bytes de imagen: {ex.Message}");
            }
        }

        public IFormFile ConvertUriToFormFile(string dataUri)
        {
            try
            {
                var splitDataUri = dataUri.Split(',');
                var header = splitDataUri[0];
                var base64Data = splitDataUri[1];
                
                var mime = header.Split(';')[0].Split(':')[1];

                var defaultName = mime switch
                {
                    "image/jpeg" => "filename.jpg",
                    "image/png" => "filename.png",
                    "application/pdf" => "filename.pdf",
                    "application/vnd.openxmlformats-officedocument.wordprocessingml.document" => "filename.docx",
                    _ => throw new Exception("Tipo MIME no soportado")
                };

                var bytes = Convert.FromBase64String(base64Data);
                var memoryStream = new MemoryStream(bytes);
                return new Files(memoryStream, mime, "name", defaultName);
            }
            catch (Exception ex)
            {
                throw new Exception($"No se pudo convertir el Data URI a IFormFile: {ex.Message}");
            }
        }
    }
}