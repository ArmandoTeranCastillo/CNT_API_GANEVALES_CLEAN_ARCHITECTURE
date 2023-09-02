
using System.Threading.Tasks;
using _2.UsersManagement.Application.DTOs.Users.Accounts.in_Controllers;

namespace _2.UsersManagement.Application.Interfaces
{
    public interface IMailService
    {
        Task SendEmail(SendConfirmationEmailDto request);
    }
}
