using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using _2.UsersManagement.Application.DTOs.Distributors.Application.Distributor.in_Controller.AssignAppointment;
using _2.UsersManagement.Application.DTOs.Distributors.Application.Distributor.in_Services;
using _2.UsersManagement.Application.Handlers;
using _2.UsersManagement.Application.Transients;
using _3.UsersManagement.Infrastructure__EFCore_.External;
using _3.UsersManagement.Infrastructure__EFCore_.Services.Distributors.Application.StoredProcedures;
using Newtonsoft.Json;
using UsersManagement.Common.Errors;
using UsersManagement.Common.Utilities;

namespace _3.UsersManagement.Infrastructure__EFCore_.Handlers
{
    public class DistributorsHandler : IDistributorsHandler
    {
        private readonly IGenericUnit _gUnit;
        private readonly IServiceUnit _sUnit;

        public DistributorsHandler(IGenericUnit gUnit, IServiceUnit sUnit)
        {
            _gUnit = gUnit;
            _sUnit = sUnit;
        }

        public async Task<object> HandleGetAll(string entity)
        {
            return entity.ToLower() switch
            {
                "prospects" => await _sUnit.Distributors.GetAllCompleteProspects(),
                "appointments" => await _sUnit.Distributors.GetAllAppointments(),
                "scholarships" => await _gUnit.Matriz
                    .GetAllById(i => i.Name, i => i.id_matriz_type == Value.Scholarship),
                _ => throw new NotFoundException(Codes.EmptyField)
            };
        }

        public async Task<object> HandleGetAllById(string entity, string reference)
        {
            return entity.ToLower() switch
            {
                "prospects" => ApplyDvSps.CallGetReportProspectsSp(reference),
                "appointments" => ApplyDvSps.CallTransTypesSp<List<GetAppointmentsDto>>(1, reference),
                "distributortypes" => await _gUnit.Matriz.GetAllById(i => i.Name, i => i.id_matriz_type == reference),
                "scholarships" =>await _gUnit.Matriz.GetAllById(i => i.Name, i => i.id_matriz_type == reference),
                "salesxp" => await _gUnit.SalesXp.GetAllById(i => i.Id, i => i.IdDistributor == reference),
                "dependents" => await _gUnit.Dependent.GetAllById(i => i.Id, i => i.IdRelation == reference),
                "vehicles" => await _gUnit.Vehicle.GetAllById(i => i.Id, i => i.IdRelation == reference),
                "references" => await _gUnit.Reference.GetAllById(i => i.Id, i => i.IdRelation == reference),
                _ => throw new NotFoundException(Codes.EmptyField)
            };
        }

        public async Task<object> HandleGetSimpleById(string entity, string reference)
        {
            return entity.ToLower() switch
            {
                "prospect" => await _sUnit.Distributors.GetSimpleCompleteProspect(reference),
                "distributorbyprospect" => await _gUnit.Distributor.GetSimpleById(i => i.IdUser == reference),
                "jobinfo" => await _gUnit.JobInfo.GetSimpleById(i => i.IdRelation == reference),
                "spouse" => await _gUnit.Spouse.GetSimpleById(i => i.IdRelation == reference),
                _ => throw new NotFoundException(Codes.EmptyField)
            };
        }

        public Task<object> HandleGetAllOneField(string entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<object> HandleGetSimpleOneFieldById(string entity, string reference)
        {
            throw new System.NotImplementedException();
        }

        public async Task<object> HandleInsertGeneric(string entity, object request)
        {
            return entity.ToLower() switch
            {
                "appointment" => await _sUnit.ApplyDv.InsertAppointment(
                    Json.Deserialize<InsertAppointmentDto>(request)),
                _ => throw new NotFoundException(Codes.EmptyField)
            };
        }
        
        public async Task<object> HandleUpdateGeneric(string entity, object request)
        {
            return entity.ToLower() switch
            {
                "appointment" => await _sUnit.Distributors.UpdateTask(
                    Json.Deserialize<UpdateAppointmentDto>(request)),
                _ => throw new NotFoundException(Codes.EmptyField)
            };
        }
    }
}