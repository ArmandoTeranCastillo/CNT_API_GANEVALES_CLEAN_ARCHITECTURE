using _1.UsersManagement.Domain.Models.Addresses;
using _1.UsersManagement.Domain.Models.Distributors;
using _1.UsersManagement.Domain.Models.Tasks;
using _2.UsersManagement.Application.DTOs.Distributors.Application.in_Controller;
using System.Collections.Generic;
using System.Threading.Tasks;
using _1.UsersManagement.Domain.Interfaces;
using _1.UsersManagement.Domain.Models.Phones;
using _1.UsersManagement.Domain.Models.Users;
using _2.UsersManagement.Application.DTOs.Distributors.Application.Distributor.in_Controller.AssignAppointment;
using _2.UsersManagement.Application.DTOs.Distributors.Application.Distributor.in_Controller.RegisterDependent;
using _2.UsersManagement.Application.DTOs.Distributors.Application.Distributor.in_Controller.RegisterDistributor;
using _2.UsersManagement.Application.DTOs.Distributors.Application.Distributor.in_Controller.RegisterJobInfo;
using _2.UsersManagement.Application.DTOs.Distributors.Application.Distributor.in_Controller.RegisterProspect;
using _2.UsersManagement.Application.DTOs.Distributors.Application.Distributor.in_Controller.RegisterReferent;
using _2.UsersManagement.Application.DTOs.Distributors.Application.Distributor.in_Controller.RegisterSalesXp;
using _2.UsersManagement.Application.DTOs.Distributors.Application.Distributor.in_Controller.RegisterSpouse;
using _2.UsersManagement.Application.DTOs.Distributors.Application.Distributor.in_Controller.RegisterSpouseJobInfo;
using _2.UsersManagement.Application.DTOs.Distributors.Application.Distributor.in_Controller.RegisterVehicle;
using _2.UsersManagement.Application.DTOs.Distributors.Application.Endorsement.inController.RegisterEndorsement;
using _2.UsersManagement.Application.DTOs.Distributors.Application.Shared;

namespace _2.UsersManagement.Application.Interfaces.Distributors.Application
{
    public interface IApplyDistributorService
    {
        //Shared
        Task<_1.UsersManagement.Domain.Models.Addresses.Addresses> InsertAddress(string idReference, InsertAddressDto addressDto);
        Task<PhoneNumbers> InsertPhone(string idProspect, InsertPhoneDto request);
        Task<PhoneNumbers> UpdatePhone(string idDv, UpdatePhoneDto phoneDto);
        Task<Prospect> UpdateProspectRelation(string idDv, string idProspect);
        Task<_1.UsersManagement.Domain.Models.Addresses.Addresses> InsertJobAddress(string idRelation, InsertAddressDto request);
        Task<List<TData>> UpdatePropertyRelation<TData, TDto>(List<TDto> requests, string propertyName)
            where TData : class, IDistributorsEntity
            where TDto : class, IRelationEntity;

        //ApplyForDistributor
        Task ValidateRegisterProspectDto(RegisterProspectDto request);
        Task<Prospect> InsertProspect(InsertProspectDto request);
        
        //AssignAppointment
        Task<_1.UsersManagement.Domain.Models.Tasks.Tasks> InsertAppointment(InsertAppointmentDto request);

        //RegisterDistributor
        Task ValidateRegisterDistributorDto(RegisterDistributorDto request);
        Task<_1.UsersManagement.Domain.Models.Distributors.Distributors> InsertDistributor(InsertDistributorDto request);
        Task<Profiles> InsertDvProfile(string idDv, Prospect prospect, InsertProfileDto profileDto);
        
        //RegisterEndorsement 
        Task ValidateRegisterEndorsementDto(RegisterEndorsementDto request);
        Task<Avals> InsertEndorsement(InsertEndorsementDto request);
        Task<Profiles> InsertEndorseProfile(string idEndorsement, Avals endorsement, InsertProfileDto profileDto);
        
        //RegisterJobInfo
        Task ValidateRegisterJobInfoDto(RegisterJobInfoDto request);
        Task<JobInfo> InsertJobInfo(InsertJobInfoDto request);

        //RegisterSalesXp
        Task ValidateRegisterSalesXpDto(List<InsertSalesXpDto> request);
        Task<List<SalesXp>> InsertSalesXp(IEnumerable<InsertSalesXpDto> request);
        Task<Prospect> UpdateProspectSalesXp(string id);

        //RegisterSpouse
        Task<Spouse> InsertSpouse(InsertSpouseDto request);

        //RegisterSpouseJobInfo
        Task ValidateRegisterSpouseJobInfo(RegisterSpouseJobInfoDto request);
        Task<JobInfo> InsertSpouseJobInfo(InsertJobInfoDto request);
        Task<TData> UpdateTotalIncoming<TData>(string idRelation) where TData : class, ITotalIncomingEntity;
        
        //RegisterDependent
        Task ValidateRegisterDependentDto(List<InsertDependentDto> requests);
        Task<List<Dependents>> InsertDependents(IEnumerable<InsertDependentDto> requests);

        //RegisterVehicle
        Task ValidateRegisterVehicleDto(List<InsertVehicleDto> requests);
        Task<List<Vehicles>> InsertVehicle(IEnumerable<InsertVehicleDto> requests);

        //RegisterReference
        Task ValidateRegisterReferenceDto(List<InsertReferenceDto> requests);
        Task<List<References>> InsertReference(IEnumerable<InsertReferenceDto> requests);
        
        //ValidateCosts
        Task<string> ValidateDvCosts(string idDistributor);
        Task<string> ValidateEndorseCosts(string idEndorsement);
    }
}
