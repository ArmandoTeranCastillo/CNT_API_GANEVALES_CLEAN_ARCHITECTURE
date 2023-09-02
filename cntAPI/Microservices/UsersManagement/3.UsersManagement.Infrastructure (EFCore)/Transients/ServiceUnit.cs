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
using _2.UsersManagement.Application.Transients;
using _3.UsersManagement.Infrastructure__EFCore_.Persistence;
using _3.UsersManagement.Infrastructure__EFCore_.Services;
using _3.UsersManagement.Infrastructure__EFCore_.Services.Addresses;
using _3.UsersManagement.Infrastructure__EFCore_.Services.Credinet;
using _3.UsersManagement.Infrastructure__EFCore_.Services.Distributors.Application;
using _3.UsersManagement.Infrastructure__EFCore_.Services.Distributors.Consults;
using _3.UsersManagement.Infrastructure__EFCore_.Services.Documents;
using _3.UsersManagement.Infrastructure__EFCore_.Services.Languages;
using _3.UsersManagement.Infrastructure__EFCore_.Services.Permissions;
using _3.UsersManagement.Infrastructure__EFCore_.Services.Tasks.Consults;
using _3.UsersManagement.Infrastructure__EFCore_.Services.Users.Accounts;
using _3.UsersManagement.Infrastructure__EFCore_.Services.Users.Auth;
using _3.UsersManagement.Infrastructure__EFCore_.Services.Users.Consults;
using Microsoft.Extensions.Configuration;

namespace _3.UsersManagement.Infrastructure__EFCore_.Transients
{
    public class ServiceUnit : IServiceUnit
    {
        public ServiceUnit(CNTContext cnt, CredinetContext credinet, IGenericUnit gUnit, IConfiguration configuration) 
        {
            Zipcode = new ZipcodeService(cnt, gUnit);
            Addresses = new AddressesService(cnt, gUnit, Zipcode);
            Curp = new CurpService(gUnit);
            Tasks = new TasksService(cnt, gUnit);
            Mail = new MailService();
            Translation = new TranslationService(gUnit, cnt);
            Success = new SuccessService(cnt, Translation, gUnit);
            Exception = new ExceptionService(cnt, gUnit);
            Users = new UsersService(cnt, gUnit, Zipcode);
            Permissions = new PermissionsService(gUnit);  
            Auth = new AuthService(cnt, gUnit, Users, Permissions, configuration);
            RegisterUser = new RegisterUserService(cnt, gUnit, Mail, Curp);
            EditUser = new EditUserService(cnt, gUnit, Curp);
            Documents = new DocumentsService(cnt, gUnit);
            ApplyDv = new ApplyDistributorService(cnt, gUnit);
            Distributors = new DistributorsService(cnt, gUnit);
            Files = new FilesService();
            AspNetUsers = new AspNetUsersService(credinet);
        }

        public IAuthService Auth { get; private set; }
        public IUsersService Users { get; private set; }
        public IEditUserService EditUser { get; private set; }
        public IRegisterUserService RegisterUser { get; private set; }
        public IDocumentsService Documents { get; private set; }
        public IFilesService Files { get; private set; }
        public IApplyDistributorService ApplyDv { get; private set; }
        public IDistributorsService Distributors { get; private set; }
        public IAddressesService Addresses { get; private set; }
        public IZipcodeService Zipcode { get; private set; }
        public ICurpService Curp { get; private set; }
        public ITasksService Tasks { get; private set; }
        public IMailService Mail { get; private set; }
        public ITranslationService Translation { get; private set; }
        public ISuccessService Success { get; private set; }
        public IExceptionService Exception { get; private set; }
        public IPermissionsService Permissions { get; private set; }
        public IAspNetUsersService AspNetUsers { get; private set; }
    }
}
