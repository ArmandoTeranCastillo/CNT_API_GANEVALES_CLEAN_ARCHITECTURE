using _1.UsersManagement.Domain.Models.Users;
using _2.UsersManagement.Application.DTOs.Addresses.In_Services;
using _2.UsersManagement.Application.DTOs.Users.Consults.in_Services;

namespace _2.UsersManagement.Application.DTOs.Users.Auth.in_Services
{
    public class GetSimpleUserCompleteDto
    {
        public UserDto User { get; set; }
        public Profiles Profile { get; set; }
        public GetAllAddressesDto Address { get; set; }
    }
}
