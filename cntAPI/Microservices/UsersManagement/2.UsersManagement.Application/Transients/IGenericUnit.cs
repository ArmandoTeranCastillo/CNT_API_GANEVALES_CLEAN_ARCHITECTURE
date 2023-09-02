using System;
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

namespace _2.UsersManagement.Application.Transients
{
    public interface IGenericUnit : IDisposable
    {
        IUserRepository User { get; }
        ICredinetRelationRepository CredinetRelation { get; }
        IUserRoleRepository UserRole { get; }
        IUserTypeRepository UserType { get; }
        IProfileRepository Profile { get; }
        IDoctoUsersRepository DoctoUser { get; }
        IDoctoReqsRepository DoctoReq { get; }
        IDoctoTypesRepository DoctoType { get; }
        IDistributorRepository Distributor { get; }
        IProspectRepository Prospect { get; }
        IAvalRepository Aval { get; }
        IJobInfoRepository JobInfo { get; }
        ISalesXpRepository SalesXp { get; }
        ISpounseRepository Spouse { get; }
        IVehicleRepository Vehicle { get; }
        IDependentRepository Dependent { get; }
        IReferenceRepository Reference { get; }
        IAddressRepository Address { get; }
        ICountryRepository Country { get; }
        IStateRepository State { get; }
        IMunicipalityRepository Municipality { get; }
        ICityRepository City { get; }
        IZipcodeRepository Zipcode { get; }
        IPhoneNumberRepository PhoneNumber { get;}
        IPhoneTypeRepository PhoneType { get;}
        IPhoneLadaRepository PhoneLada { get;}
        IRelationRepository Relation { get; }
        IMenuRepository Menu { get; }
        IViewRepository View { get; }
        IControlRepository Control { get; }
        IControlValueRepository ControlValue { get; }
        ITaskRepository Task { get; }
        ITaskMessageRepository TaskMessage { get; }
        IMatrizRepository Matriz { get; }
        IMatrizTypeRepository MatrizType { get; }
        ILanguageRepository Language { get; }
        IErrorCodeRepository ErrorCode { get; }
    }
}
