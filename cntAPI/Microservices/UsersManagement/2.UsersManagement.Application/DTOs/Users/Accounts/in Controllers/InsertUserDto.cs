namespace _2.UsersManagement.Application.DTOs.Users.Accounts.in_Controllers
{
    public class InsertUserDto
    {
        public string User { get; set; }
        public string Password { get; set; }
        public string IdUserType { get; set; } = string.Empty;
        public string IdUserRole { get; set; } = string.Empty;
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string CreatedBy { get; set; }
        public string Curp { get; set; }
    }
}
