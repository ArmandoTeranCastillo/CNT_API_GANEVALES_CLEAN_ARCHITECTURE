using System;
using System.Net;
using Web.Gateway.Common.DTOs;

namespace Web.Gateway.Common.Errors
{
    public class BadRequestException : Exception
    {
        public HttpStatusCode StatusCode { get; }
        public object json { get; }

        public BadRequestException(HttpStatusCode statusCode, object jsonContent)
        {
            StatusCode = statusCode;
            json = jsonContent;
        }

        public BadRequestException(HttpStatusCode statusCode, string code, string description)
        {
            StatusCode = statusCode;
            json = new MessageDto {code = code,  description = description, logError = "" };
        }
    }
}
