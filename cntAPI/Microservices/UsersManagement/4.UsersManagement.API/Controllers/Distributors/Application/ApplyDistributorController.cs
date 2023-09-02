using System;
using System.Linq;
using System.Threading.Tasks;
using _1.UsersManagement.Domain.Models.Distributors;
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
using _2.UsersManagement.Application.DTOs.Distributors.Application.Endorsement.InServices;
using _2.UsersManagement.Application.DTOs.Distributors.Application.in_Controller;
using _2.UsersManagement.Application.DTOs.Distributors.Application.in_Controller.RegisterVehicle;
using _2.UsersManagement.Application.Transients;
using _3.UsersManagement.Infrastructure__EFCore_.External;
using Microsoft.AspNetCore.Mvc;
using UsersManagement.Common.Errors;
using UsersManagement.Common.Utilities;

namespace _4.UsersManagement.API.Controllers.Distributors.Application
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplyDistributorController : ControllerBase
    {
        private readonly IServiceUnit _sUnit;
        public ApplyDistributorController(IServiceUnit sUnit)
        {
            _sUnit = sUnit;
        }

        [HttpPost("RegisterProspect")] // ---- Step 0   
        public async Task<IActionResult> RegisterProspect(RegisterProspectDto request, string language = Value.Language)
        {
            Http.CleanCache(Response);
            try
            {
                await _sUnit.ApplyDv.ValidateRegisterProspectDto(request);
                var prospect = await _sUnit.ApplyDv.InsertProspect(request.Prospect);
                var phone = await _sUnit.ApplyDv.InsertPhone(prospect.Id, request.Phone);
                var result = new RegisterProspectResponseDto
                {
                    IdProspect = prospect.Id,
                    IdPhone = phone.Id
                };
                return Ok(await _sUnit.Success.HandleSuccess(result, Codes.OkProspectCreated, CNames.RegisterProspect, language, request.Prospect.CreatedBy));
            }
            catch (Exception ex)
            {
                var error = await _sUnit.Exception.HandleFailure(request, ex, CNames.RegisterProspect, language, request.Prospect.CreatedBy);
                return StatusCode(error.Status, error.Error);
            }
        }
        
        [HttpPost("AssignAppointment")] // ---- Step 0.5
        public async Task<IActionResult> AssignAppointment(InsertAppointmentDto request, string language = Value.Language)
        {
            Http.CleanCache(Response);
            try
            {
                var appointment = await _sUnit.ApplyDv.InsertAppointment(request);
                return Ok(await _sUnit.Success.HandleSuccess(appointment, Codes.OkPost, CNames.AssignAppointment, language, request.CreatedBy));
            }
            catch (Exception ex)
            {
                var error = await _sUnit.Exception.HandleFailure(request, ex, CNames.AssignAppointment, language, request.CreatedBy);
                return StatusCode(error.Status, error.Error);
            }
        }
        
        [HttpPost("RegisterDistributor")] // ---- Step 1
        public async Task<IActionResult> RegisterDistributor(RegisterDistributorDto request, string language = Value.Language)
        {
            Http.CleanCache(Response);
            try
            {
                await _sUnit.ApplyDv.ValidateRegisterDistributorDto(request);
                var distributor = await _sUnit.ApplyDv.InsertDistributor(request.Distributor);
                var prospect = await _sUnit.ApplyDv.UpdateProspectRelation(distributor.Id, request.Distributor.IdProspect);
                
                var profile = await _sUnit.ApplyDv.InsertDvProfile(distributor.Id, prospect, request.Profile);
                var address = await _sUnit.ApplyDv.InsertAddress(distributor.Id, request.Address);
                await _sUnit.ApplyDv.UpdatePhone(distributor.Id, request.Phone);
                var result = new RegisterDistributorResponseDto
                {
                    IdDistributor = distributor.Id,
                    IdProfile = profile.Id,
                    IdAddress = address.Id
                };
                return Ok(await _sUnit.Success.HandleSuccess(result, Codes.OkPost, CNames.RegisterDistributor, language, request.Distributor.CreatedBy));
            }
            catch (Exception ex)
            {
                var error = await _sUnit.Exception.HandleFailure(request, ex, CNames.RegisterDistributor, language, request.Distributor.CreatedBy);
                return StatusCode(error.Status, error.Error);
            }
        }
        
        [HttpPost("RegisterEndorsement")] // ---- Step 1
        public async Task<IActionResult> RegisterEndorsement(RegisterEndorsementDto request, string language = Value.Language)
        {
            Http.CleanCache(Response);
            try
            {
                await _sUnit.ApplyDv.ValidateRegisterEndorsementDto(request);
                var endorsement = await _sUnit.ApplyDv.InsertEndorsement(request.Endorsement);
                var profile = await _sUnit.ApplyDv.InsertEndorseProfile(endorsement.Id, endorsement, request.Profile);
                var address = await _sUnit.ApplyDv.InsertAddress(endorsement.Id, request.Address);
                var phone = await _sUnit.ApplyDv.InsertPhone(endorsement.Id, request.Phone); 
                var result = new RegisterEndorsementResponseDto
                {
                    IdEndorsement = endorsement.Id,
                    IdProfile = profile.Id,
                    IdAddress = address.Id,
                    IdPhone = phone.Id
                };
                return Ok(await _sUnit.Success.HandleSuccess(result, Codes.OkPost, CNames.RegisterEndorsement, language, request.Endorsement.CreatedBy));
            }
            catch (Exception ex)
            {
                var error = await _sUnit.Exception.HandleFailure(request, ex, CNames.RegisterEndorsement, language, request.Endorsement.CreatedBy);
                return StatusCode(error.Status, error.Error);
            }
        }

        [HttpPost("RegisterJobInfo")] // ---- Step 2
        public async Task<IActionResult> RegisterJobInfo(RegisterJobInfoDto request, string language = Value.Language)
        {
            Http.CleanCache(Response);
            try
            {
                await _sUnit.ApplyDv.ValidateRegisterJobInfoDto(request);
                var jobInfo = await _sUnit.ApplyDv.InsertJobInfo(request.JobInfo);
                var address = await _sUnit.ApplyDv.InsertJobAddress(jobInfo.Id, request.Address);
                var result = new RegisterJobInfoResponseDto
                {
                    IdJobInfo = jobInfo.Id,
                    IdAddress = address.Id
                };
                return Ok(await _sUnit.Success.HandleSuccess(result, Codes.OkPost, CNames.RegisterJobInfo, language, request.JobInfo.CreatedBy));
            }
            catch (Exception ex)
            {
                var error = await _sUnit.Exception.HandleFailure(request, ex, CNames.RegisterJobInfo, language, request.JobInfo.CreatedBy);
                return StatusCode(error.Status, error.Error);
            }
        }

        [HttpPost("RegisterSalesXp")] // ---- Step 3 -- Only for Distributors
        public async Task<IActionResult> RegisterSalesXp(RegisterSalesXpDto request, string language = Value.Language)
        {
            Http.CleanCache(Response);
            try
            {
                await _sUnit.ApplyDv.ValidateRegisterSalesXpDto(request.SalesXp);
                var salesXp = await _sUnit.ApplyDv.InsertSalesXp(request.SalesXp);
                await _sUnit.ApplyDv.UpdateProspectSalesXp(salesXp[0].IdDistributor);
                var result = salesXp.Select(s => new { idSalesXp = s.Id }).ToList();
                return Ok(await _sUnit.Success.HandleSuccess(result, Codes.OkPost, CNames.RegisterSalesXp, language, request.SalesXp[0].CreatedBy));
            }
            catch (Exception ex)
            {
                var error = await _sUnit.Exception.HandleFailure(request, ex, CNames.RegisterSalesXp, language, request.SalesXp[0].CreatedBy);
                return StatusCode(error.Status, error.Error);
            }
        }

        [HttpPost("RegisterSpouse")] // ---- Step 4
        public async Task<IActionResult> RegisterSpouse(InsertSpouseDto request, string language = Value.Language)
        {
            Http.CleanCache(Response);
            try
            {
                var spouse = await _sUnit.ApplyDv.InsertSpouse(request);
                return Ok(await _sUnit.Success.HandleSuccess(spouse.Id, Codes.OkPost, CNames.RegisterSpouse, language, request.CreatedBy));
            }
            catch (Exception ex)
            {
                var error = await _sUnit.Exception.HandleFailure(request, ex, CNames.RegisterSpouse, language, request.CreatedBy);
                return StatusCode(error.Status, error.Error);
            }
        }

        [HttpPost("RegisterSpouseJobInfo")] // ---- Step 5
        public async Task<IActionResult> RegisterSpouseJobInfo(RegisterSpouseJobInfoDto request, string language = Value.Language)
        {
            Http.CleanCache(Response);
            try
            {
                await _sUnit.ApplyDv.ValidateRegisterSpouseJobInfo(request);
                var job = await _sUnit.ApplyDv.InsertSpouseJobInfo(request.JobInfo);
                var address = await _sUnit.ApplyDv.InsertJobAddress(job.Id, request.JobAddress);
                var result = new RegisterSpouseJobInfoResponseDto
                {
                    IdJobInfo = job.Id,
                    IdAddress = address.Id,
                };
                return Ok(await _sUnit.Success.HandleSuccess(result, Codes.OkPost, CNames.RegisterSpouseJobInfo, language, request.JobInfo.CreatedBy));
            }
            catch (Exception ex)
            {
                var error = await _sUnit.Exception.HandleFailure(request, ex, CNames.RegisterSpouseJobInfo, language, request.JobInfo.CreatedBy);
                return StatusCode(error.Status, error.Error);
            }
        }

        [HttpPut("UpdateTotalIncoming")] // ---- Step 5.5 
        public async Task<IActionResult> UpdateTotalIncoming(string entity, string id, string userid, string language = Value.Language)
        {
            Http.CleanCache(Response);
            try
            {
                object result;
                switch (entity)
                {
                    case "Distributor":
                        result = await _sUnit.ApplyDv.UpdateTotalIncoming<_1.UsersManagement.Domain.Models.Distributors.Distributors>(id);
                        return Ok(await _sUnit.Success.HandleSuccess(result, Codes.OkPost, CNames.UpdateTotalIncoming, language, userid));
                    case "Endorsement":
                        result = await _sUnit.ApplyDv.UpdateTotalIncoming<Avals>(id);
                        return Ok(await _sUnit.Success.HandleSuccess(result, Codes.OkPost, CNames.UpdateTotalIncoming, language, userid));
                    default: throw new NotFoundException(Codes.EmptyField);
                }
            }
            catch (Exception ex)
            {
                var error = await _sUnit.Exception.HandleFailure(id, ex, CNames.UpdateTotalIncoming, language, userid);
                return StatusCode(error.Status, error.Error);
            }
        }

        [HttpGet("CheckCredit")]
        public async Task<IActionResult> CheckCredit(string entity, string id)
        {
            string credit = null;
            if (entity is "Distributor")
            {
                credit = await _sUnit.ApplyDv.ValidateDvCosts(id);
            }
            return Ok(credit);
        }

        [HttpPost("RegisterDependents")] // ---- Step 6
        public async Task<IActionResult> RegisterDependent(RegisterDependentDto requests, string language = Value.Language)
        {
            Http.CleanCache(Response);
            try
            {
                await _sUnit.ApplyDv.ValidateRegisterDependentDto(requests.Dependents);
                var dependent = await _sUnit.ApplyDv.InsertDependents(requests.Dependents);
                await _sUnit.ApplyDv.UpdatePropertyRelation<Prospect, InsertDependentDto>(requests.Dependents, "dependents");
                var result = dependent.Select(s => new { idDependent = s.Id }).ToList();
                return Ok(await _sUnit.Success.HandleSuccess(result, Codes.OkPost, CNames.RegisterDependent, language, requests.Dependents[0].CreatedBy));
            }
            catch (Exception ex)
            {
                var error = await _sUnit.Exception.HandleFailure(requests, ex, CNames.RegisterDependent, language, requests.Dependents[0].CreatedBy);
                return StatusCode(error.Status, error.Error);
            }
        }

        [HttpPost("RegisterVehicles")] // ---- Step 7
        public async Task<IActionResult> RegisterVehicle(RegisterVehicleDto requests, string language = Value.Language)
        {
            Http.CleanCache(Response);
            try
            {
                await _sUnit.ApplyDv.ValidateRegisterVehicleDto(requests.Vehicles);
                var vehicle = await _sUnit.ApplyDv.InsertVehicle(requests.Vehicles);
                await _sUnit.ApplyDv.UpdatePropertyRelation<Prospect, InsertVehicleDto>(requests.Vehicles, "vehicles");
                var result = vehicle.Select(s => new { idVehicle = s.Id }).ToList();
                return Ok(await _sUnit.Success.HandleSuccess(result, Codes.OkPost, CNames.RegisterVehicle, language, requests.Vehicles[0].CreatedBy));
            }
            catch (Exception ex)
            {
                var error = await _sUnit.Exception.HandleFailure(requests, ex, CNames.RegisterVehicle, language, requests.Vehicles[0].CreatedBy);
                return StatusCode(error.Status, error.Error);
            }
        }

        [HttpPost("RegisterReferences")] // ---- Step 8
        public async Task<IActionResult> RegisterReference(RegisterReferenceDto requests, string language = Value.Language)
        {
            Http.CleanCache(Response);
            try
            {
                await _sUnit.ApplyDv.ValidateRegisterReferenceDto(requests.References);
                var reference = await _sUnit.ApplyDv.InsertReference(requests.References);
                var result = reference.Select(s => new { idReference = s.Id }).ToList();
                return Ok(await _sUnit.Success.HandleSuccess(result, Codes.OkPost, CNames.RegisterReference, language, requests.References[0].CreatedBy));
            }
            catch (Exception ex) 
            {
                var error = await _sUnit.Exception.HandleFailure(requests, ex, CNames.RegisterReference, language, requests.References[0].CreatedBy);
                return StatusCode(error.Status, error.Error);
            }
        }

        [HttpPost("ValidateCosts")] // ---- Step 9
        public async Task<IActionResult> ValidateCosts(string entity, string id, string userid, string language = Value.Language)
        {
            Http.CleanCache(Response);
            try
            {
                object result;
                switch (entity)
                {
                    case "Distributor":
                        result = await _sUnit.ApplyDv.ValidateDvCosts(id);
                        return Ok(await _sUnit.Success.HandleSuccess(result, Codes.OkPost, CNames.ValidateCosts, language, userid));
                    case "Endorsement":
                        result = await _sUnit.ApplyDv.ValidateEndorseCosts(id);
                        return Ok(await _sUnit.Success.HandleSuccess(result, Codes.OkPost, CNames.ValidateCosts, language, userid));
                    
                    default: throw new NotFoundException(Codes.EmptyField);
                }
              
            }
            catch (Exception ex)
            {
                var error = await _sUnit.Exception.HandleFailure(entity, ex, CNames.ValidateCosts, language, userid);
                return StatusCode(error.Status, error.Error);
            }
        }
    }
}
