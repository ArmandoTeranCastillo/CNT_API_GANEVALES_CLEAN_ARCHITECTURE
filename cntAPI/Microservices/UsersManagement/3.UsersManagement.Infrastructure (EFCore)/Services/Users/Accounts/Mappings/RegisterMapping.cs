using System;
using _1.UsersManagement.Domain.Models.Credinet;
using _1.UsersManagement.Domain.Models.Users;
using _2.UsersManagement.Application.DTOs.Permissions.In_Controller;
using _2.UsersManagement.Application.DTOs.Users.Accounts.in_Controllers;
using _2.UsersManagement.Application.DTOs.Users.Accounts.In_Services;
using UsersManagement.CURP.Enums;

namespace _3.UsersManagement.Infrastructure__EFCore_.Services.Users.Accounts.Mappings
{
    public abstract class RegisterMapping
    {
        public static PrepareRegisterInformationDto FillPrepareRegisterInformationDto(string username, string password, string curp, ValidateUserDto validateUser, string citizenship)
        {
            return new PrepareRegisterInformationDto
            {
                Username = username,
                Password = password,
                Curp = curp,
                ValidationResult = validateUser,
                Citizenship = citizenship
            };
        }
        
        public static CreateUsernameDto FillCreateUsernameDto(RegisterUserAndProfileDto request)
        {
            return new CreateUsernameDto
            {
                FirstName = request.FirstName,
                SecondName = request.MiddleName,
                FLastName = request.FLastName,
                SLastName = request.SLastName,
                CreatedBy = request.CreatedBy,
            };
        }
        
        public static GenerateCurpDto FillGenerateCurpDto(RegisterUserAndProfileDto request, int state)
        {
            return new GenerateCurpDto
            {
                Names = $"{request.FirstName} {request.MiddleName}",
                FLastName = request.FLastName,
                SLastName = request.SLastName,
                Gender = request.Gender,
                Birtdate = request.BirthDate,
                State = (Estado)state,
            };
        }
        
        public static InsertUserDto FillInsertUserDto(RegisterUserAndProfileDto request, PrepareRegisterInformationDto input)
        {
            return new InsertUserDto
            {
                User = input.Username,
                Password = input.Password,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                CreatedBy = request.CreatedBy,
                Curp = input.Curp,
            };
        }
        
        public static InsertProfileDto FillInsertProfileDto(RegisterUserAndProfileDto request, PrepareRegisterInformationDto input)
        {
            return new InsertProfileDto
            {
                FirstName = request.FirstName,
                MiddleName = request.MiddleName,
                FLastName = request.FLastName,
                SLastName = request.SLastName,
                CivilStatus = request.CivilStatus,
                Gender = request.Gender,
                Curp = input.Curp,
                IdDocNumber = string.Empty,
                DocType = string.Empty,
                BirthDate = request.BirthDate,
                IdBirthCountry = request.IdBirthCountry,
                IdBirthState = request.IdBirthState,
                IdBirthCity = string.Empty,
                Citizenship = input.Citizenship,
                ProfileImage = string.Empty,
                CreatedBy = request.CreatedBy
            };
        }
        
        public static _1.UsersManagement.Domain.Models.Users.Users FillModelUser(InsertUserDto request)
        {
            return new _1.UsersManagement.Domain.Models.Users.Users
            {
                Id = Guid.NewGuid().ToString().ToUpper(),
                User = request.User.ToUpper(),
                Password = request.Password,
                PasswordExpireDate = DateTime.Now,
                PasswordExpire = false,
                Active = true,
                Email = request.Email.ToLower(),
                PhoneNumber = request.PhoneNumber,
                Autentication = false,
                AutenticationCode = "",
                IdUserType = request.IdUserType,
                IdUserRole = request.IdUserRole,
                CreatedAt = DateTime.Now,
                CreatedBy = request.CreatedBy,
                ModiffiedAt = DateTime.Now,
                ModiffiedBy = request.CreatedBy,
                Curp = request.Curp
            };
        }
        
        public static Profiles FillModelProfile(InsertProfileDto request)
        {
            return new Profiles
            {
                Id = Guid.NewGuid().ToString().ToUpper(),
                IdUser = request.IdUser,
                FirstName = request.FirstName,
                MiddleName = request.MiddleName,
                FLastName = request.FLastName,
                SLastName = request.SLastName,
                CivilStatus = request.CivilStatus,
                Gender = request.Gender,
                Curp = request.Curp,
                IdDocNumber = request.IdDocNumber,
                IdDocType = request.DocType,
                BirthDate = request.BirthDate,
                IdBirthCountry = request.IdBirthCountry,
                IdBirthState = request.IdBirthState,
                IdBirthCity = string.Empty,
                Citizenship = request.Citizenship,
                ProfileImage = request.ProfileImage,
                Active = true,
                CreatedAt = DateTime.Now,
                CreatedBy = request.CreatedBy,
                ModiffiedAt = DateTime.Now,
                ModiffiedBy = request.CreatedBy
            };
        }
        
        public static UpdateRoleAndTypeDto FillUpdateRoleAndTypeDto(RegisterRoleAndTypeDto request)
        {
            return new UpdateRoleAndTypeDto
            {
                IdRelation = request.IdRelation,
                Role = request.Role,
                Type = request.Type,
                ModiffiedBy = request.CreatedBy
            };
        }
        
        public static InsertUsersRelationDto FillInsertUsersRelationDto(RegisterRoleAndTypeDto request)
        {
            return new InsertUsersRelationDto
            {
                IdOrigin = request.IdRelation,
                IdDestination = request.IdCredinet,
                CreatedBy = request.CreatedBy
            };
        }
        
        public static CredinetRelations FillModelRelation(InsertUsersRelationDto request)
        {
            return new CredinetRelations
            {
                Id = Guid.NewGuid().ToString().ToUpper(),
                IdCnt = request.IdOrigin,
                IdCredinet = request.IdDestination,
                TableCnt = "CNT_T_USERS",
                TableCredinet = "AspNetUsers",
                IdRelationType = "BDF0A1FE-DCCB-4B9E-BC2C-311A35E14FF1",
                Active = true,
                CreatedAt = DateTime.Now,
                CreatedBy = request.CreatedBy,
                ModiffiedAt = DateTime.Now,
                ModiffiedBy = request.CreatedBy
            };
        }
        
        public static SendConfirmationEmailDto FillSendConfirmationEmailDto(_1.UsersManagement.Domain.Models.Users.Users request, string createdBy)
        {
            return new SendConfirmationEmailDto
            {
                User = request.User,
                Password = request.Password,
                Email = request.Email,
                Createdby = createdBy
            };
        }
        
        public static SetPermissionsDto FillSetPermissionsDto(RegisterRoleAndTypeDto request)
        {
            return new SetPermissionsDto
            {
                IdUser = request.IdRelation,
                IdUserType = request.Type,
                CreatedBy = request.CreatedBy,
            };
        }
    }
}