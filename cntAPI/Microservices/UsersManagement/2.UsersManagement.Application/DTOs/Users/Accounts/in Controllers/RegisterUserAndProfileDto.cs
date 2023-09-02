using System;

namespace _2.UsersManagement.Application.DTOs.Users.Accounts.in_Controllers
{
    public class RegisterUserAndProfileDto
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string FLastName { get; set; }
        public string SLastName { get; set; }
        public string CivilStatus { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime BirthDate { get; set; }
        public string IdBirthCountry { get; set; }
        public string IdBirthState { get; set; }
        public string CreatedBy { get; set; }
    }
}
