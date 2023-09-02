using System.Threading.Tasks;
using _2.Web.Gateway.Application.DTOs;

namespace _2.Web.Gateway.Application.Interfaces.Urls
{
    public interface IRequestService
    {
        //-------------------GENERIC CONTROLLER-------------------------
        Task<object> SendGetRequest(SendRequestDto request);
        Task<object> SendPostRequest(SendRequestDto request);
        Task<object> SendPutRequest(SendRequestDto request);
        
        //------------------DOCUMENTS CONTROLLER-----------------------
        Task<object> SendRegisterDocumentsRequest(string entity, object json, string subEntity);
    }
}
