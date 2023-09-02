namespace _2.UsersManagement.Application.DTOs.Users.Accounts.in_Controllers
{
    public class SendConfirmationEmailDto
    {
        public string User { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Createdby { get; set; }
    }
}
