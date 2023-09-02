using _1.UsersManagement.Domain.Models.Users;

namespace _2.UsersManagement.Application.DTOs.Users.Consults.in_Services
{
    public class UserDto
    {
        public string Id { get; set; }
        public string User { get; set; }
        public string Email { get; set; }
        public string Curp { get; set; }
        public UserTypes UserType { get; set; }
        public UserRoles UserRole { get; set; }
        public bool Logged { get; set; }
    }
}