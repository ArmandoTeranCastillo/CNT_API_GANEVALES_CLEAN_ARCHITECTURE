namespace _2.UsersManagement.Application.DTOs.Users.Accounts.In_Services
{
    public class PrepareRegisterInformationDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Curp { get; set; }
        public ValidateUserDto ValidationResult { get; set; }
        public string Citizenship { get; set; }
    }
}
