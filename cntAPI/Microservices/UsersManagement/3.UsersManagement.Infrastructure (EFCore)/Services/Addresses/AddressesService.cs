using System;
using System.Threading.Tasks;
using _1.UsersManagement.Domain.Models.Addresses;
using _2.UsersManagement.Application.DTOs.Addresses.In_Controllers;
using _2.UsersManagement.Application.DTOs.Addresses.In_Services;
using _2.UsersManagement.Application.DTOs.Users.Accounts.In_Services;
using _2.UsersManagement.Application.Interfaces.Addresses;
using _2.UsersManagement.Application.Transients;
using _3.UsersManagement.Infrastructure__EFCore_.External;
using _3.UsersManagement.Infrastructure__EFCore_.Persistence;
using _3.UsersManagement.Infrastructure__EFCore_.Services.Addresses.Mappings;
using Newtonsoft.Json;

namespace _3.UsersManagement.Infrastructure__EFCore_.Services.Addresses
{
    public class AddressesService : IAddressesService
    {
        private readonly IGenericUnit _gUnit;
        private readonly CNTContext _cnt;
        private readonly IZipcodeService _zip;

        public AddressesService(CNTContext cnt, IGenericUnit gUnit, IZipcodeService zip)
        {
            _cnt = cnt;
            _gUnit = gUnit;
            _zip = zip;
        }

        public async Task<_1.UsersManagement.Domain.Models.Addresses.Addresses> InsertAddress(InsertAddressDto request)
        {
            await ValidateInsertAddressDto(request);
            var address = AddressesMapping.FillInsertAddressDto(request);
            _cnt.Addresses.Add(address);
            await _cnt.SaveChangesAsync();
            return address;
        }

        public async Task<GetAllAddressesDto> UpdateAddress(UpdateAddressDto request)
        {
            var address = await _gUnit.Address.UpdateEntity(request);
            return await _zip.GetAllAddressesZipcode(i => i.Id == address.IdZipCode);
        }

        //-----------------------------PRIVATE----------------------------------
        private async Task ValidateInsertAddressDto(InsertAddressDto request)
        {
            await _gUnit.User.EntityExists(request.IdRelation, "id");
            await _gUnit.Zipcode.EntityExists(request.IdZipcode, "id");
            await _gUnit.User.EntityExists(request.CreatedBy, "id");
        }
    }
}
