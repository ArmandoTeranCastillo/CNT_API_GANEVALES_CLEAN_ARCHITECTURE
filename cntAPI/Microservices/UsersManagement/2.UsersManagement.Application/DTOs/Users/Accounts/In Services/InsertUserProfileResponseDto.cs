using _1.UsersManagement.Domain.Models.Users;

namespace _2.UsersManagement.Application.DTOs.Users.Accounts.In_Services
{
    public class InsertUserProfileResponseDto
    {
        public _1.UsersManagement.Domain.Models.Users.Users User { get; set; }
        public Profiles Profile { get; set; }
    }
}
