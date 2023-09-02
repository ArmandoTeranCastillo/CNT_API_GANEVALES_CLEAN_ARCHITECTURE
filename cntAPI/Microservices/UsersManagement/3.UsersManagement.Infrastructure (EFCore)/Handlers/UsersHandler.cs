using System.Collections.Generic;
using System.Threading.Tasks;
using _1.UsersManagement.Domain.Models.Users;
using _2.UsersManagement.Application.DTOs;
using _2.UsersManagement.Application.DTOs.Users.Accounts.In_Services;
using _2.UsersManagement.Application.Handlers;
using _2.UsersManagement.Application.Transients;
using _3.UsersManagement.Infrastructure__EFCore_.External;
using UsersManagement.Common.Errors;
using UsersManagement.Common.Utilities;

namespace _3.UsersManagement.Infrastructure__EFCore_.Handlers
{
    public class UsersHandler : IUsersHandler
    {
        private readonly IGenericUnit _gUnit;
        private readonly IServiceUnit _sUnit;

        public UsersHandler(IGenericUnit gUnit, IServiceUnit sUnit)
        {
            _gUnit = gUnit;
            _sUnit = sUnit;
        }

        public async Task<object> HandleGetAll(string entity)
        {
            return entity.ToLower() switch
            {
                "userroles" => await _gUnit.UserRole.GetAll(),
                "usertypes" => await _gUnit.UserType.GetAll(),
                "profiles" => await _sUnit.Users.GetCompleteAllProfiles(),
                "users" => await _sUnit.Users.GetCompleteAllUsers(),
                "usersfromcredinet" => await _sUnit.AspNetUsers.GetAllUsers(),
                _ => throw new NotFoundException(Codes.EmptyField)
            };
        }

        public async Task<object> HandleGetAllById(string entity, string reference)
        {
            return entity.ToLower() switch
            {
                "usersbytype" => await _gUnit.User.GetAllById(i => i.Id, i => i.IdUserType == reference),
                _ => throw new NotFoundException(Codes.EmptyField)
            };
        }

        public async Task<object> HandleGetSimpleById(string entity, string reference)
        {
            return entity.ToLower() switch
            {
                "user" => await _sUnit.Users.GetSimpleCompleteUserById(i => i.Id == reference),
                "profile" => await _gUnit.Profile.GetSimpleById(i => i.Id == reference),
                _ => throw new NotFoundException(Codes.EmptyField)
            };
        }

        public Task<object> HandleGetAllOneField(string entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<object> HandleGetSimpleOneFieldById(string controller, string reference)
        {
            throw new System.NotImplementedException();
        }

        public Task<object> HandleInsertGeneric(string entity, object request)
        {
            throw new System.NotImplementedException();
        }

        public async Task<object> HandleUpdateGeneric(string entity, object request)
        {
            return entity.ToLower() switch
            {
                "user" => await _sUnit.EditUser.UpdateUser(Json.Deserialize<UpdateUserDto>(request)),
                "activateuser" => await _gUnit.User.UpdateEntity(Json.Deserialize<ActiveEntityDto>(request)),
                "profile" => await _sUnit.EditUser.UpdateProfile(Json.Deserialize<UpdateProfileDto>(request)),
                _ => throw new NotFoundException(Codes.EmptyField)
            };
        }
    }
}