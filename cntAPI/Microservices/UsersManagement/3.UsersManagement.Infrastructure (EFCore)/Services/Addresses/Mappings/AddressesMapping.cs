using System;
using _1.UsersManagement.Domain.Models.Addresses;
using _2.UsersManagement.Application.DTOs.Addresses.In_Controllers;

namespace _3.UsersManagement.Infrastructure__EFCore_.Services.Addresses.Mappings
{
    public abstract class AddressesMapping
    {
        public static _1.UsersManagement.Domain.Models.Addresses.Addresses FillInsertAddressDto(InsertAddressDto request)
        {
            return new _1.UsersManagement.Domain.Models.Addresses.Addresses
            {
                Id = Guid.NewGuid().ToString().ToUpper(),
                IdRelation = request.IdRelation,
                IdZipCode = request.IdZipcode,
                Address = request.Address,
                Active = true,
                CreatedAt = DateTime.Now,
                CreatedBy = request.CreatedBy,
                ModiffiedAt = DateTime.Now,
                ModiffiedBy = request.CreatedBy
            };
        }
    }
}