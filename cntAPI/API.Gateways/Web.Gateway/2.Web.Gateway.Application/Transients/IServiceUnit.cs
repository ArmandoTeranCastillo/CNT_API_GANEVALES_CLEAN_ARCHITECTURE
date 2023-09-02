using _2.Web.Gateway.Application.Interfaces;
using _2.Web.Gateway.Application.Interfaces.Urls;
using _2.Web.Gateway.Application.Interfaces.Users.Auth;

namespace _2.Web.Gateway.Application.Transients
{
    public interface IServiceUnit
    {
        IRequestService Request { get; }
        ITokenService Token { get; }
        IExceptionService Exception { get; }
    }
}
