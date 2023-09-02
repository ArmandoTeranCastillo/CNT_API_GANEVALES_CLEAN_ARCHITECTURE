using System;

namespace _2.UsersManagement.Application.DTOs.Distributors.Application.Shared
{
    public class InsertProfileDto
    {
        public string CivilStatus { get; set; }
        public string Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public string IdBirthCountry { get; set; }
        public string IdBirthState { get; set; }
        public string IdBirthCity { get; set; }
        public string Citizenship { get; set; }
        public string ProfileImage { get; set; }
    }
}