namespace _2.UsersManagement.Application.DTOs.Users.Accounts.in_Controllers
{
    public class RegisterRoleAndTypeDto
    {
        public string IdRelation { get; set; }
        public string Role { get; set; }
        public string Type { get; set; }
        public string IdCredinet { get; set; } = string.Empty;
        public string CreatedBy { get; set; }
    }
}
