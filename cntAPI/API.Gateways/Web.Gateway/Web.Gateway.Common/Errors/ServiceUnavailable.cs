using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Web.Gateway.Common.DTOs;

namespace Web.Gateway.Common.Errors
{
    public class ServiceUnavailable : Exception
    {
        public HttpStatusCode StatusCode { get; }
        public object json { get; }

        public ServiceUnavailable(HttpStatusCode statusCode, object jsonContent)
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

        public ServiceUnavailable(HttpStatusCode statusCode, string code, string description)
        {
            StatusCode = statusCode;
            json = new MessageDto { code = code, description = description, logError = "" };
        }
    }
}
