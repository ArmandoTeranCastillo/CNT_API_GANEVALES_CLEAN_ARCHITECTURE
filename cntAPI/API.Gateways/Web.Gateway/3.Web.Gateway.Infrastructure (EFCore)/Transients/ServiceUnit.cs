using _2.Web.Gateway.Application.Interfaces;
using _2.Web.Gateway.Application.Interfaces.Urls;
using _2.Web.Gateway.Application.Interfaces.Users.Auth;
using _2.Web.Gateway.Application.Transients;
using _3.Web.Gateway.Infrastructure__EFCore_.Services;
using _3.Web.Gateway.Infrastructure__EFCore_.Services.Urls;
using _3.Web.Gateway.Infrastructure__EFCore_.Services.Users.Auth;

namespace _3.Web.Gateway.Infrastructure__EFCore_.Transients
{
    public class ServiceUnit : IServiceUnit
    {
        private readonly IHttpService _http;
        private readonly IUrlsService _urls;

        public ServiceUnit(IHttpService http, IUrlsService urls) 
        {
            _http = http;
            _urls = urls;

            Request = new RequestService(_http, _urls);
            Token = new TokenService();
            Exception = new ExceptionService();
        }
        
        public IRequestService Request { get; private set; }
        public ITokenService Token { get; private set; }
        public IExceptionService Exception { get; private set; }
    }
}
