using System.Linq.Expressions;
using System.Threading.Tasks;
using _2.UsersManagement.Application.DTOs.Addresses.In_Controllers;
using _2.UsersManagement.Application.DTOs.Addresses.In_Services;
using _2.UsersManagement.Application.DTOs.Users.Accounts.In_Services;
using _2.UsersManagement.Application.Handlers;
using _2.UsersManagement.Application.Transients;
using _3.UsersManagement.Infrastructure__EFCore_.External;
using UsersManagement.Common.Errors;
using UsersManagement.Common.Utilities;

namespace _3.UsersManagement.Infrastructure__EFCore_.Handlers
{
    public class AddressesHandler : IAddressesHandler
    {
        private readonly IGenericUnit _gUnit;
        private readonly IServiceUnit _sUnit;

        public AddressesHandler(IGenericUnit gUnit, IServiceUnit sUnit)
        {
            _gUnit = gUnit;
            _sUnit = sUnit;
        }
        
        public Task<object> HandleGetAll(string entity)
        {
            throw new System.NotImplementedException();
        }
        
        public async Task<object> HandleGetSimpleById(string entity, string reference)
        {
            return entity.ToLower() switch
            {
                "countries" => await _gUnit.Country.GetSimpleById(i => i.Id == reference),
                "states" => await _gUnit.State.GetSimpleById(i => i.Id == reference),
                _ => throw new NotFoundException(Codes.EmptyField)
            };
        }

        public async Task<object> HandleGetAllOneField(string entity)
        {
            return entity.ToLower() switch
            {
                "countries" => await _gUnit.Country.GetAllOneField(i =>
                    new GetOneFieldDto { Id = i.Id, Field = i.Nicename }),
                _ => throw new NotFoundException(Codes.EmptyField)
            };
        }

        public async Task<object> HandleGetAllById(string entity, string reference)
        {
            return entity.ToLower() switch
            {
                "countries" => await _gUnit.Country.GetAllById(i => i.Name, i => i.Id == reference),
                "states" => await _gUnit.State.GetAllById(i => i.StateName, i => i.IdCountry == reference),
                "municipalities" => await _gUnit.Municipality.GetAllById(i => i.MunicipalityName, i => i.IdState == reference),
                "cities" => await _gUnit.City.GetAllById(i => i.CityName, i => i.IdMunicipality == reference),
                "zipcodes" => await _gUnit.Zipcode.GetAllById(i => i.CommunityName, i => i.IdCity == reference),
                "addressesbyzipcode" => await _sUnit.Zipcode.GetAllAddressesZipcode(i => i.ZipCode == reference),
                "addressesbyidzipcode" => await _sUnit.Zipcode.GetAllAddressesZipcode(i => i.Id == reference),
                "addressesbyuser" => await _gUnit.Address.GetAllById(i => i.Id, i => i.IdRelation == reference),
                _ => throw new NotFoundException(Codes.EmptyField)
            };
        }

        public async Task<object> HandleGetSimpleOneFieldById(string entity, string reference)
        {
            return entity.ToLower() switch
            {
                "zipcode" => await _gUnit.Zipcode.GetSimpleOneFieldById(
                    i => new GetOneFieldDto { Id = i.Id, Field = i.ZipCode }, i => i.Id == reference),
                _ => throw new NotFoundException(Codes.EmptyField)
            };
        }

        public async Task<object> HandleInsertGeneric(string entity, object request)
        {
            return entity.ToLower() switch
            {
                "address" => await _sUnit.Addresses.InsertAddress(Json.Deserialize<InsertAddressDto>(request)),
                _ => throw new NotFoundException(Codes.EmptyField)
            };
        }

        public async Task<object> HandleUpdateGeneric(string entity, object request)
        {
            return entity.ToLower() switch
            {
                "address" => await _sUnit.Addresses.UpdateAddress(Json.Deserialize<UpdateAddressDto>(request)),
                _ => throw new NotFoundException(Codes.EmptyField)
            };
        }
    }
}