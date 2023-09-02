using System;
using System.Data;
using _1.UsersManagement.Domain.Interfaces;
using _1.UsersManagement.Domain.Models.Addresses;
using _1.UsersManagement.Domain.Models.Distributors;
using _1.UsersManagement.Domain.Models.Phones;
using _1.UsersManagement.Domain.Models.Tasks;
using _1.UsersManagement.Domain.Models.Users;
using _2.UsersManagement.Application.DTOs.Distributors.Application.Distributor.in_Controller.AssignAppointment;
using _2.UsersManagement.Application.DTOs.Distributors.Application.Distributor.in_Controller.RegisterDependent;
using _2.UsersManagement.Application.DTOs.Distributors.Application.Distributor.in_Controller.RegisterDistributor;
using _2.UsersManagement.Application.DTOs.Distributors.Application.Distributor.in_Controller.RegisterJobInfo;
using _2.UsersManagement.Application.DTOs.Distributors.Application.Distributor.in_Controller.RegisterProspect;
using _2.UsersManagement.Application.DTOs.Distributors.Application.Distributor.in_Controller.RegisterReferent;
using _2.UsersManagement.Application.DTOs.Distributors.Application.Distributor.in_Controller.RegisterSalesXp;
using _2.UsersManagement.Application.DTOs.Distributors.Application.Distributor.in_Controller.RegisterSpouse;
using _2.UsersManagement.Application.DTOs.Distributors.Application.Distributor.in_Controller.RegisterVehicle;
using _2.UsersManagement.Application.DTOs.Distributors.Application.Distributor.in_Services;
using _2.UsersManagement.Application.DTOs.Distributors.Application.Endorsement.inController.RegisterEndorsement;
using _2.UsersManagement.Application.DTOs.Distributors.Application.in_Controller;
using _2.UsersManagement.Application.DTOs.Distributors.Application.Shared;
using Microsoft.Data.SqlClient;
using UsersManagement.Common.Utilities;

namespace _3.UsersManagement.Infrastructure__EFCore_.Services.Distributors.Application.Mappings
{
    public abstract class ApplyDvMapping
    {
        public static _1.UsersManagement.Domain.Models.Addresses.Addresses FillModelAddress(string idRelation, InsertAddressDto address)
        {
            return new _1.UsersManagement.Domain.Models.Addresses.Addresses
            {
                Id = Guid.NewGuid().ToString().ToUpper(),
                IdRelation = idRelation,
                IdZipCode = address.IdZipcode,
                Address = address.Address,
                Active = true,
                CreatedAt = DateTime.Now,
                CreatedBy = address.CreatedBy,
                ModiffiedAt= DateTime.Now,
                ModiffiedBy= address.CreatedBy,
            };
        }
        
        public static PhoneNumbers FillModelPhone(string idProspect, InsertPhoneDto request)
        {
            return new PhoneNumbers
            {
                Id = Guid.NewGuid().ToString().ToUpper(),
                IdRelation = idProspect,
                IdPhoneType = request.IdPhoneType,
                IdCountryCode = request.IdCountryCode,
                CityPhoneCode = request.CityPhoneCode,
                PhoneNumber = request.PhoneNumber,
                Active = true,
                CreatedAt = DateTime.Now,
                CreatedBy = request.CreatedBy,
                ModiffiedAt = DateTime.Now,
                ModiffiedBy = request.CreatedBy
            };
        }
        
        public static _1.UsersManagement.Domain.Models.Addresses.Addresses FillModelJobAddress(string idRelation, InsertAddressDto address)
        {
            return new _1.UsersManagement.Domain.Models.Addresses.Addresses
            {
                Id = Guid.NewGuid().ToString().ToUpper(),
                IdRelation = idRelation,
                IdZipCode = address.IdZipcode,
                Address = address.Address,
                Active = true,
                CreatedAt = DateTime.Now,
                CreatedBy = address.CreatedBy,
                ModiffiedAt= DateTime.Now,
                ModiffiedBy= address.CreatedBy,
            };
        }
        
        public static Prospect FillModelProspect(InsertProspectDto request)
        {
            return new Prospect
            {
                Id = Guid.NewGuid().ToString().ToUpper(),
                FirstName = request.FirstName,
                MiddleName = request.MiddleName,
                FLastName = request.FLastName,
                SLastName = request.SLastName,
                Curp = request.Curp,
                IdUser = "",
                IdTask = string.Empty,
                IdDistributor = string.Empty,
                Dependents = 0,
                Vehicles = 0,
                Bills = string.Empty,
                Services = string.Empty,
                House = string.Empty,
                SalesXp = false,
                Active = true,
                CreatedAt = DateTime.Now,
                CreatedBy = request.CreatedBy,
                ModiffiedAt = DateTime.Now,
                ModiffiedBy = request.CreatedBy
            };
        }
        
        public static _1.UsersManagement.Domain.Models.Tasks.Tasks FillModelTask(InsertAppointmentDto request)
        {
            return new _1.UsersManagement.Domain.Models.Tasks.Tasks
            {
                Id = Guid.NewGuid().ToString().ToUpper(),
                IdUser = request.IdUser,
                IdTaskType = string.Empty,
                Date_Start = request.date_start,
                Date_End = request.date_end,
                Location = request.Location,
                Priority = request.Priority,
                Finished = false,
                Approved = false,
                Subject = request.Subject,
                IdStatus_mtz = request.idStatus_mtz,
                Active = true,
                CreatedAt = DateTime.Now,
                CreatedBy = request.CreatedBy,
                ModiffiedAt = DateTime.Now,
                ModiffiedBy = request.CreatedBy
            };
        }
        
        public static _1.UsersManagement.Domain.Models.Distributors.Distributors FillModelDistributor(InsertDistributorDto request)
        {
            return new _1.UsersManagement.Domain.Models.Distributors.Distributors
            {
                Id = Guid.NewGuid().ToString().ToUpper(),
                Code = null,
                IdUser = request.IdProspect,
                Since = request.Since,
                id_schoolarship_mtz = request.IdSchoolarship,
                Ocupation = request.Ocupation,
                Working = request.Working,
                id_dvType_mtz = "",
                Prospect = true,
                Active = true,
                CreatedAt = DateTime.Now,
                CreatedBy = request.CreatedBy,
                ModiffiedAt = DateTime.Now,
                ModiffiedBy = request.CreatedBy
            };
        }
        
        public static Profiles FillModelDvProfile(string idDistributor, Prospect prospect, InsertProfileDto profile)
        {
            return new Profiles
            {
                Id = Guid.NewGuid().ToString().ToUpper(),
                IdUser = idDistributor,
                FirstName = prospect.FirstName,
                MiddleName = prospect.MiddleName,
                FLastName = prospect.FLastName,
                SLastName = prospect.SLastName,
                CivilStatus = profile.CivilStatus,
                Gender = profile.Gender,
                Curp = prospect.Curp,
                IdDocNumber = string.Empty,
                IdDocType = string.Empty,
                BirthDate = profile.BirthDate,
                IdBirthCountry = profile.IdBirthCountry,
                IdBirthState = profile.IdBirthState,
                IdBirthCity = profile.IdBirthCity,
                Citizenship = profile.Citizenship,
                ProfileImage = profile.ProfileImage,
                Active = true,
                CreatedAt = DateTime.Now,
                CreatedBy = prospect.CreatedBy,
                ModiffiedAt = DateTime.Now,
                ModiffiedBy = prospect.CreatedBy,
            };
        }
        
        public static Avals FillModelEndorsement(InsertEndorsementDto request)
        {
            return new Avals
            {
                Id = Guid.NewGuid().ToString().ToUpper(),
                FirstName = request.FirstName,
                MiddleName = request.MiddleName,
                FLastName = request.FlastName,
                SLastName = request.SlastName,
                Curp = request.Curp,
                IdDistributor = request.IdDistributor,
                Dependents = request.Dependents,
                Vehicles = request.Vehicles,
                Bills = request.Bills,
                House = request.House,
                SalesXp = request.SalesXp,
                Active = true,
                CreatedAt = DateTime.Now,
                CreatedBy = request.CreatedBy,
                ModiffiedAt = DateTime.Now,
                ModiffiedBy = request.CreatedBy
            };
        }
        
        public static Profiles FillModelEndorseProfile(string idDistributor, Avals endorsement, InsertProfileDto profile)
        {
            return new Profiles
            {
                Id = Guid.NewGuid().ToString().ToUpper(),
                IdUser = idDistributor,
                FirstName = endorsement.FirstName,
                MiddleName = endorsement.MiddleName,
                FLastName = endorsement.FLastName,
                SLastName = endorsement.SLastName,
                CivilStatus = profile.CivilStatus,
                Gender = profile.Gender,
                Curp = endorsement.Curp,
                IdDocNumber = string.Empty,
                IdDocType = string.Empty,
                BirthDate = profile.BirthDate,
                IdBirthCountry = profile.IdBirthCountry,
                IdBirthState = profile.IdBirthState,
                IdBirthCity = profile.IdBirthCity,
                Citizenship = profile.Citizenship,
                ProfileImage = profile.ProfileImage,
                Active = true,
                CreatedAt = DateTime.Now,
                CreatedBy = endorsement.CreatedBy,
                ModiffiedAt = DateTime.Now,
                ModiffiedBy = endorsement.CreatedBy,
            };
        }
        
        public static JobInfo FillModelJobInfo(InsertJobInfoDto request)
        {
            return new JobInfo
            {
                Id = Guid.NewGuid().ToString().ToUpper(),
                Name = request.Name,
                Title = request.Title,
                Since = request.Since,
                Checkin = request.CheckinTime,
                Checkout = request.CheckoutTime,
                Workingdays = request.Workingdays,
                Responsable = request.Responsable,
                IdRelation = request.IdRelation,
                Income = request.Income,
                Active = true,
                CreatedAt = DateTime.Now,
                CreatedBy = request.CreatedBy,
                ModiffiedAt = DateTime.Now,
                ModiffiedBy = request.CreatedBy,
            };
        }
        
        public static SalesXp FillModelSalesXp(InsertSalesXpDto request)
        {
            return new SalesXp
            {
                Id = Guid.NewGuid().ToString().ToUpper(),
                IdDistributor = request.IdDistributor,
                company_name = request.company_name,
                Since = request.Since,
                Limit = request.Limit,
                Comission = request.Comission,
                Other = request.Other,
                Active = true,
                CreatedAt = DateTime.Now,
                CreatedBy = request.CreatedBy,
                ModiffiedAt= DateTime.Now,
                ModiffiedBy = request.CreatedBy,
            };
        }
        
        public static Spouse FillModelSpouse(InsertSpouseDto request)
        {
            return new Spouse
            {
                Id = Guid.NewGuid().ToString().ToUpper(),
                IdRelation = request.IdRelation,
                FirstName = request.FirstName,
                MiddleName = request.MiddleName,
                FLastName = request.FLastName,
                SLastName = request.SLastName,
                BirthDate = request.BirthDate,
                Ocupation = request.Ocupation,
                Curp = request.Curp,
                Active = true,
                CreatedAt = DateTime.Now,
                CreatedBy = request.CreatedBy,
                ModiffiedAt = DateTime.Now,
                ModiffiedBy = request.CreatedBy,
            };
        }
        
        public static Dependents FillModelDependent(InsertDependentDto request)
        {
            return new Dependents
            {
                Id = Guid.NewGuid().ToString().ToUpper(),
                IdRelation = request.idRelation,
                FirstName = request.FirstName,
                MiddleName = request.MiddleName,
                FLastName = request.FLastName,
                SLastName = request.SLastName,
                BirthDate = request.BirthDate,
                id_dependent_mtz = request.id_dependent_mtz,
                Ocupation = request.Ocupation,
                Income = request.Income,
                Active = true,
                CreatedAt = DateTime.Now,
                CreatedBy = request.CreatedBy,
                ModiffiedAt = DateTime.Now,
                ModiffiedBy = request.CreatedBy
            };
        }
        
        public static Vehicles FillModelVehicle(InsertVehicleDto request)
        {
            return new Vehicles
            {
                Id = Guid.NewGuid().ToString().ToUpper(),
                IdRelation = request.idRelation,
                Brand = request.Brand,
                Serie = request.Serie,
                Price = request.Price,
                Model = request.Model,
                Active = true,
                CreatedAt = DateTime.Now,
                CreatedBy = request.CreatedBy,
                ModiffiedAt = DateTime.Now,
                ModiffiedBy = request.CreatedBy
            };
        }
        
        public static References FillModelReference(InsertReferenceDto request)
        {
            return new References
            {
                Id = Guid.NewGuid().ToString().ToUpper(),
                IdRelation = request.IdRelation,
                FirstName = request.FirstName,
                MiddleName = request.MiddleName,
                FLastName = request.FLastName,
                SLastName = request.SLastName,
                id_partnership_mtz = request.id_partnership_mtz,
                Address = request.Address,
                Phone = request.Phone,
                Active = true,
                CreatedAt = DateTime.Now,
                CreatedBy = request.CreatedBy,
                ModiffiedAt = DateTime.Now,
                ModiffiedBy = request.CreatedBy,
            };
        }
        
        public static ValidateCostsDto FillValidateCostsDto(IDistributorsEntity prospect, string totalIncome)
        {
            return new ValidateCostsDto
            {
                HasSalesXp = prospect.SalesXp,
                Has5K = decimal.TryParse(totalIncome, out var income) && income >= 5000,
                Has10K = decimal.TryParse(totalIncome, out income) && income >= 10000
            };
        }

    }
}