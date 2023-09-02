using _2.UsersManagement.Application.Interfaces.Addresses.Repositories;
using _2.UsersManagement.Application.Interfaces.ControlValues.Repositories;
using _2.UsersManagement.Application.Interfaces.Credinet.Repositories;
using _2.UsersManagement.Application.Interfaces.Distributors.Consults.Repositories;
using _2.UsersManagement.Application.Interfaces.Documents.Repositories;
using _2.UsersManagement.Application.Interfaces.Exceptions.Repositories;
using _2.UsersManagement.Application.Interfaces.Languages.Repositories;
using _2.UsersManagement.Application.Interfaces.Matriz.Repositories;
using _2.UsersManagement.Application.Interfaces.Permissions.Repositories;
using _2.UsersManagement.Application.Interfaces.Phones.Repositories;
using _2.UsersManagement.Application.Interfaces.Tasks.Consults.Repositories;
using _2.UsersManagement.Application.Interfaces.Users.Consults.Repositories;
using _2.UsersManagement.Application.Transients;
using _3.UsersManagement.Infrastructure__EFCore_.Persistence;
using _3.UsersManagement.Infrastructure__EFCore_.Repositories.Addresses;
using _3.UsersManagement.Infrastructure__EFCore_.Repositories.ControlValues;
using _3.UsersManagement.Infrastructure__EFCore_.Repositories.Credinet;
using _3.UsersManagement.Infrastructure__EFCore_.Repositories.Distributors;
using _3.UsersManagement.Infrastructure__EFCore_.Repositories.Documents;
using _3.UsersManagement.Infrastructure__EFCore_.Repositories.Exceptions;
using _3.UsersManagement.Infrastructure__EFCore_.Repositories.Languages;
using _3.UsersManagement.Infrastructure__EFCore_.Repositories.Matriz;
using _3.UsersManagement.Infrastructure__EFCore_.Repositories.Permissions;
using _3.UsersManagement.Infrastructure__EFCore_.Repositories.Phones;
using _3.UsersManagement.Infrastructure__EFCore_.Repositories.Tasks;
using _3.UsersManagement.Infrastructure__EFCore_.Repositories.Users;

namespace _3.UsersManagement.Infrastructure__EFCore_.Transients
{
    public class GenericUnit : IGenericUnit
    {
        private readonly CNTContext _cnt;

        public GenericUnit(CNTContext cnt)
        {
            _cnt = cnt;
                
            User = new UserRepository(_cnt);
            CredinetRelation = new CredinetRelationRepository(_cnt);
            UserRole = new UserRoleRepository(_cnt);
            UserType = new UserTypeRepository(_cnt);
            Profile = new ProfileRepository(_cnt);
            DoctoUser = new DoctoUsersRepository(_cnt);
            DoctoType = new DoctoTypesRepository(_cnt);
            DoctoReq = new DoctoReqsRepository(_cnt);
            Distributor = new DistributorRepository(_cnt);
            Prospect = new ProspectRepository(_cnt);
            Aval = new AvalRepository(_cnt);
            JobInfo = new JobInfoRepository(_cnt);
            SalesXp = new SalesXpRepository(_cnt);
            Spouse = new SpounseRepository(_cnt);
            Vehicle = new VehicleRepository(_cnt);
            Dependent = new DependentRepository(_cnt);
            Reference = new ReferenceRepository(_cnt);
            Address = new AddressRepository(_cnt);
            Country = new CountryRepository(_cnt);
            State = new StateRepository(_cnt);
            Municipality = new MunicipalityRepository(_cnt);
            City = new CityRepository(_cnt);
            Zipcode = new ZipcodeRepository(_cnt);
            PhoneNumber = new PhoneNumberRepository(_cnt);
            PhoneType = new PhoneTypeRepository(_cnt);
            PhoneLada = new PhoneLadaRepository(_cnt);
            Relation = new RelationRepository(_cnt);
            Menu = new MenuRepository(_cnt);
            View = new ViewRepository(_cnt);
            Control = new ControlRepository(_cnt);
            ControlValue = new ControlValueRepository(_cnt);
            Task = new TaskRepository(_cnt);
            TaskMessage = new TaskMessageRepository(_cnt);
            Matriz = new MatrizRepository(_cnt);
            MatrizType = new MatrizTypeRepository(_cnt);
            Language = new LanguageRepository(_cnt);
            ErrorCode = new ErrorCodeRepository(_cnt);
        }

        public IUserRepository User { get; private set; }
        public ICredinetRelationRepository CredinetRelation { get; private set; }
        public IUserRoleRepository UserRole { get; private set; }
        public IUserTypeRepository UserType { get; private set; }
        public IProfileRepository Profile { get; private set; }
        public IDoctoUsersRepository DoctoUser { get; private set; }
        public IDoctoReqsRepository DoctoReq { get; private set; }
        public IDoctoTypesRepository DoctoType { get; private set; }
        public IDistributorRepository Distributor { get; private set; }
        public IProspectRepository Prospect { get; private set; }
        public IAvalRepository Aval { get; private set; }
        public IJobInfoRepository JobInfo { get; private set; }
        public ISalesXpRepository SalesXp { get; private set; }
        public ISpounseRepository Spouse { get; private set; }
        public IVehicleRepository Vehicle { get; private set; }
        public IDependentRepository Dependent { get; private set; }
        public IReferenceRepository Reference { get; private set; }
        public IAddressRepository Address { get; private set; }
        public ICountryRepository Country { get; private set; }
        public IStateRepository State { get; private set; }
        public IMunicipalityRepository Municipality { get; private set; }
        public ICityRepository City { get; private set; }
        public IZipcodeRepository Zipcode { get; private set; }
        public IPhoneNumberRepository PhoneNumber { get; private set; }
        public IPhoneTypeRepository PhoneType { get; private set; }
        public IPhoneLadaRepository PhoneLada { get; private set; }
        public IRelationRepository Relation { get; private set; }
        public IMenuRepository Menu { get; private set; }
        public IViewRepository View { get; private set; }
        public IControlRepository Control { get; private set; }
        public IControlValueRepository ControlValue { get; private set; }
        public ITaskRepository Task { get; private set; }
        public ITaskMessageRepository TaskMessage { get; private set; }
        public IMatrizRepository Matriz { get; private set; }
        public IMatrizTypeRepository MatrizType { get; private set; }
        public ILanguageRepository Language { get; private set; }
        public IErrorCodeRepository ErrorCode { get; private set; }

        public void Dispose()
        {
            _cnt.Dispose();
        }
    }
}
