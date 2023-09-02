using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using _1.UsersManagement.Domain.Models.Users;
using _2.UsersManagement.Application.DTOs.Users.Auth.in_Services;
using _2.UsersManagement.Application.DTOs.Users.Consults.in_Services;

namespace _2.UsersManagement.Application.Interfaces.Users.Consults
{
    public interface IUsersService
    {
        Task<IEnumerable<_1.UsersManagement.Domain.Models.Users.Users>> GetCompleteAllUsers();
        Task<IEnumerable<GetAllProfilesCompleteDto>> GetCompleteAllProfiles();
        Task<GetSimpleUserCompleteDto> GetSimpleCompleteUserById(Expression<Func<UserDto, bool>> filter);
    }
}
