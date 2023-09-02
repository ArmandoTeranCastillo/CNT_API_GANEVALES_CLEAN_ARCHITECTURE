using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace _3.UsersManagement.Infrastructure__EFCore_.External
{
    public class Files : IFormFile
    {
        private readonly MemoryStream _stream;
        private readonly string _contentType;
        private readonly string _name;
        private readonly string _fileName;

        public Files(MemoryStream stream, string contentType, string name, string fileName)
        {
            _stream = stream;
            _contentType = contentType;
            _name = name;
            _fileName = fileName;
        }

        public string ContentType => _contentType;

        public string ContentDisposition => throw new System.NotImplementedException();

        public string FileName => _fileName;

        public IHeaderDictionary Headers => throw new System.NotImplementedException();

        public long Length => _stream.Length;

        public string Name => _name;

        public void CopyTo(Stream target)
        {
            _stream.Position = 0;
            _stream.CopyTo(target);
        }

        public async System.Threading.Tasks.Task CopyToAsync(Stream target, System.Threading.CancellationToken cancellationToken = default)
        {
            _stream.Position = 0;
            await _stream.CopyToAsync(target, cancellationToken);
        }

        public Stream OpenReadStream()
        {
            _stream.Position = 0;
            return _stream;
        }
    }
}