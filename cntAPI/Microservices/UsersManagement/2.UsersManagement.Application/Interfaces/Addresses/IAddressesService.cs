using System.Threading.Tasks;
using _1.UsersManagement.Domain.Models.Addresses;
using _2.UsersManagement.Application.DTOs.Addresses.In_Controllers;
using _2.UsersManagement.Application.DTOs.Addresses.In_Services;
using _2.UsersManagement.Application.DTOs.Users.Accounts.In_Services;

namespace _2.UsersManagement.Application.Interfaces.Addresses
{
    public interface IAddressesService
    {
        Task<_1.UsersManagement.Domain.Models.Addresses.Addresses> InsertAddress(InsertAddressDto request);
        Task<GetAllAddressesDto> UpdateAddress(UpdateAddressDto request);
    }
}
