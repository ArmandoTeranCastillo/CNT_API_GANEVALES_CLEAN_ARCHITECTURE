using System.Collections.Generic;
using System.Threading.Tasks;
using _2.UsersManagement.Application.DTOs.Credinet.in_Services;

namespace _2.UsersManagement.Application.Interfaces.Credinet
{
    public interface IAspNetUsersService
    {
        Task<IEnumerable<ASPNetUserDto>> GetAllUsers();
    }
}
