using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using _2.UsersManagement.Application.DTOs.Addresses.In_Services;
using _2.UsersManagement.Application.DTOs.Users.Auth.in_Services;
using _2.UsersManagement.Application.DTOs.Users.Consults.in_Services;
using _2.UsersManagement.Application.Interfaces.Addresses;
using _2.UsersManagement.Application.Interfaces.Users.Consults;
using _2.UsersManagement.Application.Transients;
using _3.UsersManagement.Infrastructure__EFCore_.Persistence;
using UsersManagement.Common.Errors;
using UsersManagement.Common.Utilities;

namespace _3.UsersManagement.Infrastructure__EFCore_.Services.Users.Consults
{
    public class UsersService : IUsersService
    {
        private readonly CNTContext _cnt;
        private readonly IGenericUnit _gUnit;
        private readonly IZipcodeService _zip;

        public UsersService(CNTContext cnt, IGenericUnit gUnit, IZipcodeService zip)
        {
            _cnt = cnt;
            _gUnit = gUnit;
            _zip = zip;
        }

        //------------------------------PUBLIC-------------------------------------

        public async Task<IEnumerable<_1.UsersManagement.Domain.Models.Users.Users>> GetCompleteAllUsers()
        {
            var users = await _cnt.Users
                .Include(i => i.UserRole)
                .Include(i => i.UserType)
                .ToListAsync();
            return users;
        }

        public async Task<IEnumerable<GetAllProfilesCompleteDto>> GetCompleteAllProfiles()
        {
            var profileList = await _cnt.Profiles
                .Include(i => i.User)
                .Include(i => i.User.UserRole)
                .Include(i => i.User.UserType)
                .ToListAsync();

            return profileList.Select(profile => new GetAllProfilesCompleteDto
            {
                Id = profile.Id,
                IdUser = profile.IdUser,
                User = profile.User?.User,
                FirstName = profile.FirstName,
                FLastName = profile.FLastName,
                Role = profile.User?.UserRole?.UserRole,
                Type = profile.User?.UserType?.UserType,
                Active = profile.User?.Active.ToString()
            }).ToList();
        }

        public async Task<GetSimpleUserCompleteDto> GetSimpleCompleteUserById(Expression<Func<UserDto, bool>> filter)
        {
                var user = await _cnt.Users
                    .Include(i => i.UserType)
                    .Include(i => i.UserRole)
                    .Select(i => new UserDto
                    {
                        Id = i.Id,
                        User = i.User,
                        Email = i.Email,
                        Curp = i.Curp,
                        UserType = i.UserType,
                        UserRole = i.UserRole,
                        Logged = i.Logged
                    })
                    .FirstOrDefaultAsync(filter);
                if (user is null) throw new NotFoundException(Codes.EmptyField);

                var profile = await _cnt.Profiles.FirstOrDefaultAsync(i => i.IdUser == user.Id);
                
                GetAllAddressesDto location;
                try
                {
                    var address = await _gUnit.Address.GetSimpleById(i => i.IdRelation == user.Id);
                    location = await _zip.GetAllAddressesZipcode(i => i.Id == address.IdZipCode);
                }
                catch (Exception)
                {
                    throw new NotFoundException(Codes.UserHasNotAddress);
                }

                return new GetSimpleUserCompleteDto
                {
                    User = user,
                    Profile = profile,
                    Address = location
                };
        }
    }
}
