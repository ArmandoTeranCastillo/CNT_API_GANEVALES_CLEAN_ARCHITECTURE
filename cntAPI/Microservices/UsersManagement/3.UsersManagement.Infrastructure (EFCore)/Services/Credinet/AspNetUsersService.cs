using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using _2.UsersManagement.Application.DTOs.Credinet.in_Services;
using _2.UsersManagement.Application.Interfaces.Credinet;
using _3.UsersManagement.Infrastructure__EFCore_.Persistence;
using Microsoft.EntityFrameworkCore;

namespace _3.UsersManagement.Infrastructure__EFCore_.Services.Credinet
{
    public class AspNetUsersService : IAspNetUsersService
    {
        private readonly CredinetContext _credinet;

        public AspNetUsersService(CredinetContext credinet)
        {
            _credinet = credinet;
        }

        public async Task<IEnumerable<ASPNetUserDto>> GetAllUsers()
        {
            var users = await _credinet.AspNetUsers.ToListAsync();
            var filteredUsers = users.Where(i => i.Active && !IsNumeric(i.UserName))
                .Select(i => new ASPNetUserDto
                {
                    Id = i.Id,
                    UserName = i.UserName,
                })
                .OrderBy(i => i.UserName);

            return filteredUsers;
        }
    
    //------------------------PRIVATE------------------------------------

        private static bool IsNumeric(string str)
        {
            return str is not null && Regex.IsMatch(str, @"^\d+$");
        }
    }
}
