using _1.UsersManagement.Domain.Models.Addresses;
using _1.UsersManagement.Domain.Models.Distributors;
using _1.UsersManagement.Domain.Models.Tasks;
using _2.UsersManagement.Application.DTOs.Distributors.Application.in_Controller;
using _2.UsersManagement.Application.Interfaces.Distributors.Application;
using System;
using System.Collections.Generic;
using System.Linq;
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
using _2.UsersManagement.Application.DTOs.Distributors.Application.Distributor.in_Services;
using _2.UsersManagement.Application.DTOs.Distributors.Application.Endorsement.inController.RegisterEndorsement;
using _2.UsersManagement.Application.DTOs.Distributors.Application.Shared;
using _2.UsersManagement.Application.Transients;
using _3.UsersManagement.Infrastructure__EFCore_.External;
using _3.UsersManagement.Infrastructure__EFCore_.Persistence;
using _3.UsersManagement.Infrastructure__EFCore_.Services.Distributors.Application.Mappings;
using _3.UsersManagement.Infrastructure__EFCore_.Services.Distributors.Application.StoredProcedures;
using _3.UsersManagement.Infrastructure__EFCore_.Services.Distributors.Application.Utilities;
using UsersManagement.Common.Errors;
using UsersManagement.Common.Utilities;

namespace _3.UsersManagement.Infrastructure__EFCore_.Services.Distributors.Application
{
    public class ApplyDistributorService : IApplyDistributorService
    {
        private readonly IGenericUnit _gUnit;
        private readonly CNTContext _cnt;

        public ApplyDistributorService(CNTContext cnt, IGenericUnit gUnit)
        {
            _cnt = cnt;
            _gUnit = gUnit;
        }
        
        //Shared--------------------------------------------
        public async Task<_1.UsersManagement.Domain.Models.Addresses.Addresses> InsertAddress(string idRelation, InsertAddressDto addressDto)
        {
            var address = ApplyDvMapping.FillModelAddress(idRelation, addressDto);
            _cnt.Addresses.Add(address);
            await _cnt.SaveChangesAsync();
            return address;
        }
        
        public async Task<PhoneNumbers> InsertPhone(string idRelation, InsertPhoneDto request)
        {
            var phone = ApplyDvMapping.FillModelPhone(idRelation, request);
            phone.PhoneNumber = Cipher.StringEncrypting(phone.PhoneNumber);
            _cnt.PhoneNumbers.Add(phone);
            await _cnt.SaveChangesAsync();
            return phone;
        }
        
        public async Task<PhoneNumbers> UpdatePhone(string idRelation, UpdatePhoneDto phoneDto)
        {
            var phone = await _gUnit.PhoneNumber.GetSimpleById(i => i.Id == phoneDto.Id);
            phone.IdRelation = idRelation;
            await _cnt.SaveChangesAsync();
            return phone;
        }
        
        public async Task<Prospect> UpdateProspectRelation(string idRelation, string idProspect)
        {
            var prospect = await _gUnit.Prospect.GetSimpleById(i => i.Id == idProspect);
            prospect.IdDistributor = idRelation;
            await _cnt.SaveChangesAsync();
            return prospect;
        }
        
        public async Task<_1.UsersManagement.Domain.Models.Addresses.Addresses> InsertJobAddress(string idRelation, InsertAddressDto request)
        {
            var address = ApplyDvMapping.FillModelJobAddress(idRelation, request);
            _cnt.Addresses.Add(address);
            await _cnt.SaveChangesAsync();
            return address;
        }
        
        public async Task<List<TData>> UpdatePropertyRelation<TData, TDto>(List<TDto> requests, string propertyName)
            where TData : class, IDistributorsEntity
            where TDto : class, IRelationEntity
        {
            var entityList = new List<TData>();

            foreach (var request in requests)
            {
                var entityRepository = new GenericService<TData>(_cnt);
                var entity = await entityRepository.GetSimpleById(i => i.IdDistributor == request.idRelation);

                var property = typeof(TData).GetProperty(propertyName);
                if (property != null && property.PropertyType == typeof(int))
                {
                    var currentValue = (int)property.GetValue(entity)!;
                    property.SetValue(entity, currentValue + 1);
                }
                await _cnt.SaveChangesAsync();
                entityList.Add(entity);
            }
            return entityList;
        }

        //RegisterProspect---------------------------
        public async Task ValidateRegisterProspectDto(RegisterProspectDto request)
        {
            await ValidateInsertProspectDto(request);
            
        }

        private async Task ValidateInsertProspectDto(RegisterProspectDto request)
        {
            var validation = ApplyDvSps.CallSearchCurpSp(request.Prospect.Curp);
            if (!string.IsNullOrWhiteSpace(validation))
            {
                var exceptionMessage = await _gUnit.TaskMessage.GetEntityProperty(validation, "idTask", "message");
                throw new BadRequestException(exceptionMessage);
            }
            await _gUnit.Prospect.EntityNotExists(request.Prospect.Curp, "curp");
            await ValidateEndorseNotExists(request.Prospect.Curp);
            await _gUnit.User.EntityExists(request.Prospect.CreatedBy, "id");
        }

        private async Task ValidateInsertPhoneDto(RegisterProspectDto request)
        {
            await _gUnit.PhoneLada.EntityExists(request.Phone.CityPhoneCode, "phoneLada");
            await _gUnit.Country.EntityExists(request.Phone.IdCountryCode, "id");
            await _gUnit.PhoneType.EntityExists(request.Phone.IdPhoneType, "id");
            await _gUnit.User.EntityExists(request.Phone.CreatedBy, "id");
        }

        public async Task<Prospect> InsertProspect(InsertProspectDto request)
        {
            var prospect = ApplyDvMapping.FillModelProspect(request);
            prospect.IdUser = ApplyDvSps.CallGetUserToProspectSp();
            prospect = ApplyDvUtilities.EncryptProspect(prospect);
            _cnt.Prospects.Add(prospect);
            await _cnt.SaveChangesAsync();
            return prospect;
        }

        //AssignAppointment---------------------------------
        public async Task<_1.UsersManagement.Domain.Models.Tasks.Tasks> InsertAppointment(InsertAppointmentDto request)
        {
            var appointment = ApplyDvMapping.FillModelTask(request);
            _cnt.Tasks.Add(appointment);
            await _cnt.SaveChangesAsync();
            return appointment;
        }

        //Register Distributor
        public async Task ValidateRegisterDistributorDto(RegisterDistributorDto request)
        {
            await ValidateInsertDistributorDto(request.Distributor);
            await ValidateDvInsertAddressDto(request.Address);
            await ValidateDvInsertProfileDto(request.Profile);
            await ValidateUpdateDvPhoneDto(request.Phone);
        }

        public async Task<_1.UsersManagement.Domain.Models.Distributors.Distributors> InsertDistributor(InsertDistributorDto request)
        {
            var distributor = ApplyDvMapping.FillModelDistributor(request);
            _cnt.Distributors.Add(distributor);
            await _cnt.SaveChangesAsync();
            return distributor;
        }
        
        public async Task<Profiles> InsertDvProfile(string idDistributor, Prospect prospect, InsertProfileDto profileDto)
        {
            var profile = ApplyDvMapping.FillModelDvProfile(idDistributor, prospect, profileDto);
            profile.IdDocNumber = Cipher.StringEncrypting(profile.IdDocNumber);
            _cnt.Profiles.Add(profile);
            await _cnt.SaveChangesAsync();
            return profile;
        }

        //RegisterEndorsement
        public async Task ValidateRegisterEndorsementDto(RegisterEndorsementDto request)
        {
            await ValidateInsertEndorsementDto(request.Endorsement);
            await ValidateDvInsertProfileDto(request.Profile);
            await ValidateDvInsertAddressDto(request.Address);
        }
        
        public async Task<Avals> InsertEndorsement(InsertEndorsementDto request)
        {
            var endorsement = ApplyDvMapping.FillModelEndorsement(request);
            _cnt.Avals.Add(endorsement);
            await _cnt.SaveChangesAsync();
            return endorsement;
        }
        
        public async Task<Profiles> InsertEndorseProfile(string idEndorsement, Avals endorsement, InsertProfileDto profileDto)
        {
            var profile = ApplyDvMapping.FillModelEndorseProfile(idEndorsement, endorsement, profileDto);
            profile.IdDocNumber = Cipher.StringEncrypting(profile.IdDocNumber);
            _cnt.Profiles.Add(profile);
            await _cnt.SaveChangesAsync();
            return profile;
        }

        //RegisterJobInfo
        public async Task ValidateRegisterJobInfoDto(RegisterJobInfoDto request)
        {
            await ValidateInsertJobInfoDto(request.JobInfo);
            await ValidateInsertJobAddressDto(request.Address);
        }
        
        public async Task<JobInfo> InsertJobInfo(InsertJobInfoDto request)
        {
            var job = ApplyDvMapping.FillModelJobInfo(request);
            job.Workingdays = ApplyDvUtilities.CreateSequenceDays(job.Workingdays);
            job.Income = Cipher.StringEncrypting(job.Income);
            _cnt.JobInfo.Add(job);
            await _cnt.SaveChangesAsync();
            return job;
        }

        //RegisterSalesXp
        public async Task ValidateRegisterSalesXpDto(List<InsertSalesXpDto> requests)
        {
            foreach (var request in requests)
            {
                await ValidateInsertSalesXpDto(request);
            }
        }
        
        public async Task<List<SalesXp>> InsertSalesXp(IEnumerable<InsertSalesXpDto> requests)
        {
            var salesList = new List<SalesXp>();
            foreach (var sales in requests.Select(ApplyDvMapping.FillModelSalesXp))
            {
                var saleXp = ApplyDvUtilities.EncryptSalesXp(sales);
                _cnt.SalesXps.Add(sales);
                salesList.Add(saleXp);
            }
            await _cnt.SaveChangesAsync();
            return salesList;
        }

        public async Task<Prospect> UpdateProspectSalesXp(string id)
        {
            var distributor=  await _gUnit.Distributor.GetSimpleById(i => i.Id == id);
            var prospect = await _gUnit.Prospect.GetSimpleById(i => i.Id == distributor.IdUser);
            prospect.SalesXp = true;
            await _cnt.SaveChangesAsync();
            return prospect;
        }

        //RegisterSpouse
        public async Task<Spouse> InsertSpouse(InsertSpouseDto request)
        {
            await ValidateInsertSpouseDto(request); 
            var spouse = ApplyDvMapping.FillModelSpouse(request);    
            _cnt.Spouses.Add(spouse);
            await _cnt.SaveChangesAsync();
            return spouse;
        }

        //RegisterSpouseJobInfo
        public async Task ValidateRegisterSpouseJobInfo(RegisterSpouseJobInfoDto request)
        {
            await ValidateInsertSpouseJobInfoDto(request.JobInfo);
            await ValidateInsertSpouseAddressDto(request.JobAddress);
        }

        public async Task<JobInfo> InsertSpouseJobInfo (InsertJobInfoDto request)
        {
            var job = ApplyDvMapping.FillModelJobInfo(request);
            job.Workingdays = ApplyDvUtilities.CreateSequenceDays(job.Workingdays);
            job.Income = Cipher.StringEncrypting(job.Income);
            _cnt.JobInfo.Add(job);
            await _cnt.SaveChangesAsync();
            return job;
        }

        //UpdateTotalIncoming
        public async Task<TData> UpdateTotalIncoming<TData>(string idRelation) where TData : class, ITotalIncomingEntity
        {
            var repository = new GenericService<TData>(_cnt);
            var entity = await repository.GetSimpleById(i => i.Id == idRelation);
            var totalIncome = await GetTotalIncoming(idRelation, entity);
            entity.TotalIncoming =  Cipher.StringEncrypting(totalIncome);
            await _cnt.SaveChangesAsync();
            return entity;
        }
        
        //RegisterDependent
        public async Task ValidateRegisterDependentDto(List<InsertDependentDto> requests)
        {
            foreach (var request in requests)
            {
                await ValidateInsertDependentDto(request);
            }
        }
       
        public async Task<List<Dependents>> InsertDependents(IEnumerable<InsertDependentDto> requests)
        {
            var dependentsList = new List<Dependents>();
            foreach (var dependents in requests.Select(ApplyDvMapping.FillModelDependent))
            {
                _cnt.Dependents.Add(dependents);
                dependentsList.Add(dependents);
            }
            await _cnt.SaveChangesAsync();
            return dependentsList;
        }
        
        //RegisterVehicle
        public async Task ValidateRegisterVehicleDto(List<InsertVehicleDto> requests)
        {
            foreach (var request in requests)
            {
                await ValidateInsertVehicleDto(request);
            }
        }
        
        public async Task<List<Vehicles>> InsertVehicle(IEnumerable<InsertVehicleDto> requests)
        {
            var vehiclesList = new List<Vehicles>();
            foreach (var vehicles in requests.Select(ApplyDvMapping.FillModelVehicle))
            {
                vehicles.Price = Cipher.StringEncrypting(vehicles.Price);
                _cnt.Vehicles.Add(vehicles);
                vehiclesList.Add(vehicles);
            }
            await _cnt.SaveChangesAsync();
            return vehiclesList;
        }

        //RegisterReference
        public async Task ValidateRegisterReferenceDto(List<InsertReferenceDto> requests)
        {
            foreach (var request in requests)
            {
                await ValidateInsertReferenceDto(request);
            }
        }
        
        public async Task<List<References>> InsertReference(IEnumerable<InsertReferenceDto> requests)
        {
            var referencesList = new List<References>();
            foreach (var references in requests.Select(ApplyDvMapping.FillModelReference))
            {
                _cnt.References.Add(references);
                referencesList.Add(references);
            }
            await _cnt.SaveChangesAsync();
            return referencesList;
        }
        
        //ValidateCosts
        public async Task<string> ValidateDvCosts(string idDistributor)
        {
            var distributor = await _gUnit.Distributor.GetSimpleById(i => i.Id == idDistributor);
            var prospect= await _gUnit.Prospect.GetSimpleById(i => i.Id == distributor.IdUser);
            var totalIncome = Cipher.StringEncrypting(distributor.TotalIncoming);
            var validateCosts = ApplyDvMapping.FillValidateCostsDto(prospect, totalIncome);
            return await CheckCreditEligibility(validateCosts);
        }

        public async Task<string> ValidateEndorseCosts(string idEndorsement)
        {
            var endorsement = await _gUnit.Aval.GetSimpleById(i => i.Id == idEndorsement);
            var totalIncome = Cipher.StringEncrypting(endorsement.TotalIncoming);
            var validateCosts = ApplyDvMapping.FillValidateCostsDto(endorsement, totalIncome);
            return await CheckCreditEligibility(validateCosts);
        }
        
        //------------------------------------------PRIVATE----------------------------------------------------
        //RegisterProspect--------------------------------
        private async Task ValidateEndorseNotExists(string curp)
        {
            try
            {
                await _gUnit.Aval.EntityNotExists(curp, "curp");
            }
            catch (Exception)
            {
                if (await IsDistributorDebtor(curp))
                {
                    throw new BadRequestException(Codes.EntityAlreadyCreated);
                }
            }
        }
        
        private async Task<bool> IsDistributorDebtor(string curp)
        {
            var endorsement = await _gUnit.Aval.GetSimpleById(i => i.Curp == curp);
            var distributor = await _gUnit.Distributor.GetSimpleById(i => i.Id == endorsement.IdDistributor);
            return distributor.Active is false;
        }

        //RegisterDistributor------------------------
        private async Task ValidateInsertDistributorDto(InsertDistributorDto request)
        {
            await _gUnit.Distributor.EntityNotExists(request.IdProspect, "idUser");
            await _gUnit.Prospect.EntityExists(request.IdProspect, "id");
            await _gUnit.Matriz.EntityExists(request.IdSchoolarship, "id");
            await _gUnit.User.EntityExists(request.CreatedBy, "id");
        }

        private async Task ValidateDvInsertAddressDto(InsertAddressDto request)
        {
            await _gUnit.Zipcode.EntityExists(request.IdZipcode, "id");
        }

        private async Task ValidateDvInsertProfileDto(InsertProfileDto request)
        {
            await _gUnit.Country.EntityExists(request.IdBirthCountry, "id");
            await _gUnit.State.EntityExists(request.IdBirthState, "id");
            await _gUnit.City.EntityExists(request.IdBirthCity, "id");
        }
        
        private async Task ValidateUpdateDvPhoneDto(UpdatePhoneDto request)
        {
            await _gUnit.PhoneNumber.EntityExists(request.Id, "id");
        }
        
        //RegisterEndorsement
        private async Task ValidateInsertEndorsementDto(InsertEndorsementDto request)
        {
            await _gUnit.Distributor.EntityExists(request.IdDistributor, "id");
            await _gUnit.User.EntityExists(request.CreatedBy, "id");
        }
        
        //RegisterJobInfo
        private async Task ValidateInsertJobInfoDto(InsertJobInfoDto request)
        {
            await _gUnit.JobInfo.EntityNotExists(request.IdRelation, "idRelation");
            await _gUnit.User.EntityExists(request.CreatedBy, "id");
        }
        private async Task ValidateInsertJobAddressDto(InsertAddressDto request)
        {
            await _gUnit.Zipcode.EntityExists(request.IdZipcode, "id");
        }
        
        //RegisterSalesXp
        private async Task ValidateInsertSalesXpDto(InsertSalesXpDto request)
        {
            await _gUnit.Distributor.EntityExists(request.IdDistributor, "id");
            await _gUnit.User.EntityExists(request.CreatedBy, "id");
        }
        
        //RegisterSpouse
        private async Task ValidateInsertSpouseDto(InsertSpouseDto request)
        {
            await _gUnit.User.EntityExists(request.CreatedBy, "id");
        }

        //RegisterSpouseJobInfo
        private async Task ValidateInsertSpouseJobInfoDto(InsertJobInfoDto request)
        {
            await _gUnit.JobInfo.EntityNotExists(request.IdRelation, "idRelation");
            await _gUnit.Spouse.EntityExists(request.IdRelation, "id");
            await _gUnit.User.EntityExists(request.CreatedBy, "id");
        }
        
        private async Task ValidateInsertSpouseAddressDto(InsertAddressDto request)
        {
            await _gUnit.Zipcode.EntityExists(request.IdZipcode, "id");
        }
        
        //UpdateTotalIncoming
        private async Task<string> GetTotalIncoming(string idRelation, object entity)
        {
            var spouse = await _gUnit.Spouse.GetSimpleById(i => i.Id == idRelation);
            JobInfo spouseJobInfo = null;
            if (spouse != null)
            {
                spouseJobInfo = await _gUnit.JobInfo.GetSimpleById(i => i.IdRelation == spouse.Id);
            }
            var jobInfo = await _gUnit.JobInfo.GetSimpleById(i => i.IdRelation == idRelation);
            
            var salesXps = entity != null ? await _gUnit.SalesXp
                .GetAllById(i => i.Id, i => i.IdDistributor == idRelation) : new List<SalesXp>();
            return ApplyDvUtilities.SumTotalIncome(spouseJobInfo, jobInfo, salesXps);
        }
        
        //RegisterDependent
        private async Task ValidateInsertDependentDto(InsertDependentDto request)
        {
            await _gUnit.Matriz.EntityExists(request.id_dependent_mtz, "id");
            await _gUnit.User.EntityExists(request.CreatedBy, "id");
        }
        
        //RegisterVehicle
        private async Task ValidateInsertVehicleDto(InsertVehicleDto request)
        {
            await _gUnit.Distributor.EntityExists(request.idRelation, "id");
            await _gUnit.User.EntityExists(request.CreatedBy, "id");
        }
        
        //RegisterReference
        private async Task ValidateInsertReferenceDto(InsertReferenceDto request)
        {
            await _gUnit.Distributor.EntityExists(request.IdRelation, "id");
            await _gUnit.Matriz.EntityExists(request.id_partnership_mtz, "id");
            await _gUnit.User.EntityExists(request.CreatedBy, "id");
        }
        
        //ValidateCosts
        private async Task<string> CheckCreditEligibility(ValidateCostsDto validateCosts)
        {
            if (validateCosts.CanBeDistributorCredit)
            {
                return await _gUnit.Matriz.GetEntityProperty(Value.DistributorCredit, "id", "name");
            }
            if(validateCosts.CanBePersonalCredit)
            { 
                return await _gUnit.Matriz.GetEntityProperty(Value.PersonalCredit, "id", "name");
            }
            return await _gUnit.Matriz.GetEntityProperty(Value.Denied, "id", "name");
        }
    }
}

