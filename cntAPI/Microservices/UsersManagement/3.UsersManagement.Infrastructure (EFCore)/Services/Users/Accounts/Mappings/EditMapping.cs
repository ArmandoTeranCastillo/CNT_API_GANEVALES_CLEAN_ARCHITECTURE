using System;
using _1.UsersManagement.Domain.Models.Users;
using _2.UsersManagement.Application.DTOs.Users.Accounts.in_Controllers;
using _2.UsersManagement.Application.DTOs.Users.Accounts.In_Services;
using UsersManagement.CURP.Enums;

namespace _3.UsersManagement.Infrastructure__EFCore_.Services.Users.Accounts.Mappings
{
    public abstract class EditMapping
    {
        public static SendConfirmationEmailDto FillSendConfirmationEmailDto(ChangePasswordDto request, _1.UsersManagement.Domain.Models.Users.Users user)
        {
            return new SendConfirmationEmailDto
            {
                User = user.User,
                Email = request.Email,
                Password = user.Password,
                Createdby = request.CreatedBy
            };
        }
        
        public static GenerateCurpDto FillGenerateCurpDto(UpdateProfileDto request, int state)
        {
            if (request.BirthDate == null) return null;
            var createCurp = new GenerateCurpDto
            {
                Names = $"{request.FirstName} {request.MiddleName}",
                FLastName = request.FLastName,
                SLastName = request.SLastName,
                Gender = string.Empty,
                Birtdate = (DateTime)request.BirthDate,
                State = (Estado)state,
            };
            return createCurp;
        }
    }
}