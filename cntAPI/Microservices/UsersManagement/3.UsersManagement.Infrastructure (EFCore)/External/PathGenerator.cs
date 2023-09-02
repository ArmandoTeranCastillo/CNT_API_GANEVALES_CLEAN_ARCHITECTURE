using System;
using System.IO;

namespace _3.UsersManagement.Infrastructure__EFCore_.External
{
    public class PathGenerator
    {
        private readonly string _baseDir;
        public PathGenerator(string baseDir)
        {
            _baseDir = baseDir;
        }
        
        public string GeneratePath(
            string userType,
            string userId,
            string documentFolder,
            string documentName,
            string extension,
            string subfolder = null,
            string subfolderUserId = null
        )
        {
            var path = _baseDir;
            path = Path.Combine(path, userType, userId);
            if (subfolder != null)
            {
                path = Path.Combine(path, subfolder);
                if (subfolderUserId != null)
                {
                    path = Path.Combine(path, subfolderUserId);
                }
            }
            path = Path.Combine(path, documentFolder);
            Directory.CreateDirectory(path);
            return Path.Combine(path, $"{documentName}{extension}");
        }
        
        public string GenerateUrl(
            string userType,
            string userId,
            string documentFolder,
            string documentName,
            string extension,
            string subfolder = null,
            string subfolderUserId = null
        )
        {
            var path = _baseDir;
            path = Path.Combine(path, userType, userId);
            if (subfolder != null)
            {
                path = Path.Combine(path, subfolder);
                if (subfolderUserId != null)
                {
                    path = Path.Combine(path, subfolderUserId);
                }
            }
            path = Path.Combine(path, documentFolder);
            path = Path.Combine(path, $"{documentName}{extension}");
            return Uri.EscapeUriString(path.Replace(@"\", @"/"));
        }
    }
}