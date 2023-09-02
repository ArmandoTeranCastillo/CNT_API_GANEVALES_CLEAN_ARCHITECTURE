using System.Collections.Generic;

namespace _2.Web.Gateway.Application.Interfaces.Urls
{
    public interface IUrlsService
    {
        string GetUrl(string methodName, Dictionary<string, object> parameters);
        string GetUrl(string methodName, object[] parameters);
    }
}
