using _2.Web.Gateway.Application.DTOs;

namespace _3.Web.Gateway.Infrastructure__EFCore_.Services.Urls.Mappings
{
    public class RequestMappings
    {
        public static SendRequestDto FillRequestDto(string host, string url, string userId = null,  string controllerName = null, string entity = null, string reference = null, object request = null,  string language = null)
        {
            return new SendRequestDto
            {
                ControllerName = controllerName,
                Entity = entity,
                Reference = reference,
                Request = request,
                UserId = userId,
                Host = host,
                Language = language,
                Url = url
            };
        }
    }
}