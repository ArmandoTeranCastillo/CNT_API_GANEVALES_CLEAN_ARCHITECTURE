using System.Threading.Tasks;
using _2.UsersManagement.Application.DTOs.Users.Accounts.in_Controllers;
using _2.UsersManagement.Application.DTOs.Users.Accounts.In_Services;

namespace _2.UsersManagement.Application.Interfaces.Users.Accounts
{
    public interface IRegisterUserService
    {
        void ValidateRegisterUserAndProfileDto(RegisterUserAndProfileDto request);
        Task<PrepareRegisterInformationDto> PrepareRegisterUserAndProfile(RegisterUserAndProfileDto request);
        Task<InsertUserProfileResponseDto> InsertUserAndProfile(RegisterUserAndProfileDto request, PrepareRegisterInformationDto inputRegister);
        Task<UpdateRoleTypeResponseDto> UpdateRoleAndType(RegisterRoleAndTypeDto request);
        Task GeneratePermissions(RegisterRoleAndTypeDto request);
        Task InsertUserRelation(RegisterRoleAndTypeDto request);
        Task SendConfirmationEmail(RegisterRoleAndTypeDto request);
    }
}
