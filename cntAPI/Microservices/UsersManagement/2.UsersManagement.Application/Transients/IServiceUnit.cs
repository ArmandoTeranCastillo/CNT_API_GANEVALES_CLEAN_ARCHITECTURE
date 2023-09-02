using _2.UsersManagement.Application.Interfaces;
using _2.UsersManagement.Application.Interfaces.Addresses;
using _2.UsersManagement.Application.Interfaces.Credinet;
using _2.UsersManagement.Application.Interfaces.Distributors.Application;
using _2.UsersManagement.Application.Interfaces.Distributors.Consults;
using _2.UsersManagement.Application.Interfaces.Documents;
using _2.UsersManagement.Application.Interfaces.Languages;
using _2.UsersManagement.Application.Interfaces.Permissions;
using _2.UsersManagement.Application.Interfaces.Tasks;
using _2.UsersManagement.Application.Interfaces.Tasks.Consults;
using _2.UsersManagement.Application.Interfaces.Users.Accounts;
using _2.UsersManagement.Application.Interfaces.Users.Auth;
using _2.UsersManagement.Application.Interfaces.Users.Consults;

namespace _2.UsersManagement.Application.Transients
{
    public interface IServiceUnit
    {
        IAuthService Auth { get; }
        IUsersService Users { get; }
        IEditUserService EditUser { get; }
        IRegisterUserService RegisterUser { get; }
        IDocumentsService Documents { get; }
        IFilesService Files { get; }
        IApplyDistributorService ApplyDv { get; }
        IDistributorsService Distributors { get; }
        IAddressesService Addresses { get; }
        IZipcodeService Zipcode { get; }
        ICurpService Curp { get; }
        ITasksService Tasks { get; }
        IMailService Mail { get; }
        ITranslationService Translation { get; }
        ISuccessService Success { get; }
        IExceptionService Exception { get; }
        IPermissionsService Permissions { get; }
        IAspNetUsersService AspNetUsers { get; }
    }
}
