using System;

namespace _2.UsersManagement.Application.DTOs.Users.Accounts.in_Controllers
{
    public class InsertProfileDto
    {
        public string IdUser { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string FLastName { get; set; }
        public string SLastName { get; set; }
        public string CivilStatus { get; set; }
        public string Gender { get; set; }
        public string Curp { get; set; }
        public string IdDocNumber { get; set; }
        public string DocType { get; set; }
        public DateTime BirthDate { get; set; }
        public string IdBirthCountry { get; set; }
        public string IdBirthState { get; set; }
        public string IdBirthCity { get; set; }
        public string Citizenship { get; set; }
        public string ProfileImage { get; set; }
        public string CreatedBy { get; set; }
    }
}
