namespace _2.UsersManagement.Application.DTOs.Users.Accounts.in_Controllers
{
    public class CreateUsernameDto
    {
        public string FirstName { get; set; } = string.Empty;
        public string SecondName { get; set; } = string.Empty;
        public string FLastName { get; set; } = string.Empty;
        public string SLastName { get; set; } = string.Empty;
        public string CreatedBy { get; set; }
    }
}
