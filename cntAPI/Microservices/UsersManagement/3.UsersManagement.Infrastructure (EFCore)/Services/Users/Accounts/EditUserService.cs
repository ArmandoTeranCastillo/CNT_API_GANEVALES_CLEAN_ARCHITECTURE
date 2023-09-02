using _2.UsersManagement.Application.DTOs.Users.Accounts.in_Controllers;
using _2.UsersManagement.Application.DTOs.Users.Accounts.In_Services;
using _2.UsersManagement.Application.Interfaces;
using _2.UsersManagement.Application.Interfaces.Users.Accounts;
using System.Threading.Tasks;
using _1.UsersManagement.Domain.Models.Users;
using _2.UsersManagement.Application.Transients;
using _3.UsersManagement.Infrastructure__EFCore_.External;
using _3.UsersManagement.Infrastructure__EFCore_.Persistence;
using _3.UsersManagement.Infrastructure__EFCore_.Services.Users.Accounts.Mappings;
using UsersManagement.CURP.Enums;

namespace _3.UsersManagement.Infrastructure__EFCore_.Services.Users.Accounts
{
    public class EditUserService : IEditUserService
    {
        private readonly IGenericUnit _gUnit;
        private readonly CNTContext _cnt;
        private readonly ICurpService _curp;

        public EditUserService(CNTContext cnt, IGenericUnit gUnit, ICurpService curp)
        {
            _cnt = cnt;
            _gUnit = gUnit;
            _curp = curp;
        }

        //ChangePassword--------------------------------
        public async Task<_1.UsersManagement.Domain.Models.Users.Users> EditPasswordByAdmin(ChangePasswordDto request)
        {
            var user = await _gUnit.User.GetSimpleById(i => i.Id == request.IdUser);
            user.OldPassword = user.Password;
            user.Password = Cipher.StringEncrypting(request.NewPassword);
            await _cnt.SaveChangesAsync();
            return user;
        }

        //UpdateUser------------------------------------------------------------------
        private async Task ValidateUpdateUserDto(UpdateUserDto request)
        {
            await _gUnit.User.EntityExists(request.id, "id");
            await _gUnit.User.EntityExists(request.modiffiedBy, "id");
        }

        //UpdateProfile-----------------------------------------------------------------
        private async Task ValidateUpdateProfileDto(UpdateProfileDto request)
        {
            await _gUnit.Profile.EntityExists(request.id, "id");

            if (request.IdBirthCountry is not null)
                await _gUnit.Country.EntityExists(request.IdBirthCountry, "id");

            if (request.IdBirthState is not null)
                await _gUnit.State.EntityExists(request.IdBirthState, "id");

            await _gUnit.User.EntityExists(request.modiffiedBy, "id");
        }
        
        public async Task<_1.UsersManagement.Domain.Models.Users.Users> UpdateUser(UpdateUserDto request)
        {
            await ValidateUpdateUserDto(request);
            return await _gUnit.User.UpdateEntity(request);
        }

        public async Task<Profiles> UpdateProfile(UpdateProfileDto request)
        {
            await ValidateUpdateProfileDto(request);
            if (request.Curp is not null) return await _gUnit.Profile.UpdateEntity(request);
            var stateEnum = await _curp.GetStateEnum(request.IdBirthState);
            var createCurp = EditMapping.FillGenerateCurpDto(request, stateEnum);
            createCurp.Gender = await _gUnit.Profile.GetEntityProperty(request.id, "id", "gender");
            request.Curp = _curp.CreateCurp(createCurp);
            return await _gUnit.Profile.UpdateEntity(request);
        }

        
        //UpdateAddress---------------------------------------------------------------
        public async Task ValidateUpdateAddressDto(UpdateAddressDto request)
        {
            await _gUnit.Address.EntityExists(request.id, "id");

            if (request.IdRelation is not null)
                await _gUnit.User.EntityExists(request.IdRelation, "id");

            if (request.IdZipCode is not null)
                await _gUnit.Zipcode.EntityExists(request.IdZipCode, "id");

            await _gUnit.User.EntityExists(request.modiffiedBy, "id");
        }
    }
}
