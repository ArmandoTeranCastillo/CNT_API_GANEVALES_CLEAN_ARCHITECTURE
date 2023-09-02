using System;
using System.Net;
using Web.Gateway.Common.DTOs;

namespace Web.Gateway.Common.Errors
{
    public class NotFoundException : Exception
    {
        public HttpStatusCode StatusCode { get; }
        public object json { get; }

        public NotFoundException(HttpStatusCode statusCode, object jsonContent)
        {
            try
            {
                StatusCode = statusCode;
                json = jsonContent;
            }
            catch (Exception ex)
            {
                throw new Exception("Error en el constructor de NotFoundException.", ex);
            }
        }

        public NotFoundException(HttpStatusCode statusCode, string code, string description)
        {
            StatusCode = statusCode;
            json = new MessageDto { code = code, description = description, logError = "" };
        }
    }
}
