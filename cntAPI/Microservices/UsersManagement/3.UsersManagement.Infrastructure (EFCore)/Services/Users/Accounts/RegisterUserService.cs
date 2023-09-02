using _2.UsersManagement.Application.Interfaces;
using _2.UsersManagement.Application.Interfaces.Users.Accounts;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Threading.Tasks;
using _2.UsersManagement.Application.DTOs.Addresses.In_Services;
using _2.UsersManagement.Application.DTOs.Permissions.In_Controller;
using _2.UsersManagement.Application.DTOs.Users.Accounts.in_Controllers;
using _2.UsersManagement.Application.DTOs.Users.Accounts.In_Services;
using _2.UsersManagement.Application.Transients;
using _3.UsersManagement.Infrastructure__EFCore_.External;
using _3.UsersManagement.Infrastructure__EFCore_.Persistence;
using _3.UsersManagement.Infrastructure__EFCore_.Services.Users.Accounts.Mappings;
using _3.UsersManagement.Infrastructure__EFCore_.Services.Users.Accounts.StoredProcedures;
using _3.UsersManagement.Infrastructure__EFCore_.Services.Users.Accounts.Utilities;
using UsersManagement.Common.Errors;
using UsersManagement.Common.Utilities;

namespace _3.UsersManagement.Infrastructure__EFCore_.Services.Users.Accounts
{
    public class RegisterUserService : IRegisterUserService
    {
        private readonly CNTContext _cnt;
        private readonly IGenericUnit _gUnit;
        private readonly IMailService _mail;
        private readonly ICurpService _curp;

        public RegisterUserService(CNTContext cnt, IGenericUnit gUnit, IMailService mail, ICurpService curp)
        {
            _cnt = cnt;
            _gUnit = gUnit;
            _mail = mail;
            _curp = curp;
        }

        //RegisterUserAndProfile--------------
        public void ValidateRegisterUserAndProfileDto(RegisterUserAndProfileDto request)
        {
            Functions.IsNullOrEmpty(request.FirstName);
            Functions.IsNullOrEmpty(request.FLastName);
            Functions.IsNullOrEmpty(request.CivilStatus);
            Functions.IsNullOrEmpty(request.Email);
            Functions.IsNullOrEmpty(request.BirthDate.ToString(CultureInfo.InvariantCulture));
            Functions.IsNullOrEmpty(request.Gender);
            Functions.IsNullOrEmpty(request.PhoneNumber);
            Functions.IsNullOrEmpty(request.IdBirthCountry);
            Functions.IsNullOrEmpty(request.IdBirthState);
            Functions.IsNullOrEmpty(request.CreatedBy);
        }

        public async Task<PrepareRegisterInformationDto> PrepareRegisterUserAndProfile(RegisterUserAndProfileDto request)
        {
            var username = PrepareUsername(request);
            var password = PreparePassword();
            var curp = await PrepareCurp(request);
            var validateUser = RegisterSps.CallValidateUsersSp(username, curp);
            var citizenship = await PrepareCitizen(request);
            
            return RegisterMapping.
                FillPrepareRegisterInformationDto(username, password, curp, validateUser, citizenship);
        }

        public async Task<InsertUserProfileResponseDto> InsertUserAndProfile(RegisterUserAndProfileDto request, PrepareRegisterInformationDto inputRegister)
        {
            var user = RegisterMapping.FillInsertUserDto(request, inputRegister);
            var profile = RegisterMapping.FillInsertProfileDto(request, inputRegister);

            await ValidateInsertUserDto(user);
            await ValidateInsertProfileDto(profile);

            return await InsertUserAndProfile(user, profile);
        }

        //RegisterRoleAndType----------------
        public async Task<UpdateRoleTypeResponseDto> UpdateRoleAndType(RegisterRoleAndTypeDto request)
        {
            var inputResult = RegisterMapping.FillUpdateRoleAndTypeDto(request);
            await ValidateUpdateRoleTypeDto(inputResult);
            return await UpdateRoleAndType(inputResult);
        }

        public async Task InsertUserRelation(RegisterRoleAndTypeDto request)
        {
            var inputRelation = RegisterMapping.FillInsertUsersRelationDto(request);
            await InsertUserRelation(inputRelation);
        }

        public async Task GeneratePermissions(RegisterRoleAndTypeDto request)
        {
            var permissions = RegisterMapping.FillSetPermissionsDto(request);
            await SetPermissions(permissions);
        }

        public async Task SendConfirmationEmail(RegisterRoleAndTypeDto request)
        {
            var user = await _gUnit.User.GetSimpleById(i => i.Id == request.IdRelation);
            var inputEmail = RegisterMapping.FillSendConfirmationEmailDto(user, request.CreatedBy);
            await _mail.SendEmail(inputEmail);
        }

        //-----------------------------------------------PRIVATE-----------------------------------------------------
        //RegisterUserAndProfile-------------------------------
        private static string PrepareUsername(RegisterUserAndProfileDto request)
        {
            var createUsername = RegisterMapping.FillCreateUsernameDto(request);
            return RegisterUtilities.CreateUsername(createUsername);
        }

        private static string PreparePassword()
        {
            return RegisterUtilities.CreateRandomPassword();
        }

        private async Task<string> PrepareCurp(RegisterUserAndProfileDto request)
        {
            var stateEnum = await _curp.GetStateEnum(request.IdBirthState);
            var createCurp = RegisterMapping.FillGenerateCurpDto(request, stateEnum);
            return CreateCurp(createCurp);
        }
        
        private async Task<string> PrepareCitizen(RegisterUserAndProfileDto request)
        {
            await _gUnit.Country.EntityExists(request.IdBirthCountry, "id");
            var citizen = await _gUnit.Country
                            .GetSimpleOneFieldById(i => new GetOneFieldDto { Id = i.Id, Field = i.Iso3 }, i => i.Id == request.IdBirthCountry);
            return citizen.Field;
        }
        
        private string CreateCurp(GenerateCurpDto request)
        {
            return _curp.CreateCurp(request);
        }
        
        private async Task ValidateInsertUserDto(InsertUserDto request)
        {
            await _gUnit.Profile.EntityNotExists(request.Curp, "curp");
            await _gUnit.User.EntityExists(request.CreatedBy, "id");
            Functions.IsNumeric(request.PhoneNumber);
        }

        private async Task ValidateInsertProfileDto(InsertProfileDto request)
        {
            await _gUnit.Country.EntityExists(request.IdBirthCountry, "id");
            await _gUnit.State.EntityExists(request.IdBirthState, "id");
            await _gUnit.User.EntityExists(request.CreatedBy, "id");
            IsTooLong(request);
        }
        
        private async Task<InsertUserProfileResponseDto> InsertUserAndProfile(InsertUserDto user, InsertProfileDto profile)
        {
            await ValidateInsertUserDto(user);
            await ValidateInsertProfileDto(profile);

            var newUser = RegisterMapping.FillModelUser(user);
            newUser.Password = Cipher.StringEncrypting(newUser.Password);
            profile.IdUser = newUser.Id;
            var newProfile = RegisterMapping.FillModelProfile(profile);

            _cnt.Users.Add(newUser);
            _cnt.Profiles.Add(newProfile);
            await _cnt.SaveChangesAsync();

            return new InsertUserProfileResponseDto
            {
                User = newUser,
                Profile = newProfile
            };
        }

        //RegisterUserRoleAndUserType-----------------------------------------
        private async Task ValidateUpdateRoleTypeDto(UpdateRoleAndTypeDto request)
        {
            await _gUnit.User.EntityExists(request.IdRelation, "id");
            await _gUnit.UserRole.EntityExists(request.Role, "id");
            await _gUnit.UserType.EntityExists(request.Type, "id");
            await _gUnit.User.EntityExists(request.ModiffiedBy, "id");
        }

        private async Task ValidateUserRelationDto(InsertUsersRelationDto request)
        {
            await _gUnit.CredinetRelation.EntityNotExists(request.IdDestination, "idCredinet");
        }
        
        private async Task<UpdateRoleTypeResponseDto> UpdateModelUser(UpdateRoleAndTypeDto request)
        {
            var user = await _cnt.Users
                        .FirstOrDefaultAsync(i => i.Id == request.IdRelation);

            user.IdUserRole = request.Role;
            user.IdUserType = request.Type;

            return new UpdateRoleTypeResponseDto
            {
                User = user.User,
                Role = user.IdUserRole,
                Type = user.IdUserType
            };
        }

        private async Task<UpdateRoleTypeResponseDto> UpdateRoleAndType(UpdateRoleAndTypeDto request)
        {
            await ValidateUpdateRoleTypeDto(request);
            var user = await UpdateModelUser(request);
            await _cnt.SaveChangesAsync();
            return user;
        }
        
        private async Task InsertUserRelation(InsertUsersRelationDto request)
        {
            await ValidateUserRelationDto(request);
            var relation = RegisterMapping.FillModelRelation(request);
            _cnt.CredinetRelations.Add(relation);
            await _cnt.SaveChangesAsync();
        }
        
        private async Task SetPermissions(SetPermissionsDto request)
        {
            await _cnt.Database.ExecuteSqlRawAsync("CNT_SP_GENERATEPERMISSONS @idUser, @idUserType, @createdBy",
                new SqlParameter("@idUser", request.IdUser),
                new SqlParameter("@idUserType", request.IdUserType),
                new SqlParameter("@createdBy", request.CreatedBy));
        }

        

        private static void IsTooLong(InsertProfileDto request)
        {
            if (request.Citizenship.Length > 3) throw new BadRequestException(Codes.FieldTooLong);
        }
    }
}
