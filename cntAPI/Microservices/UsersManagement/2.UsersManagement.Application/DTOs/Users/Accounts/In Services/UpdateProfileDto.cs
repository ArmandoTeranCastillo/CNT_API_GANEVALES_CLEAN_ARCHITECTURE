using _1.UsersManagement.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2.UsersManagement.Application.DTOs.Users.Accounts.In_Services
{
    public class UpdateProfileDto : IUpdateEntity
    {
        public string id {  get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string FLastName { get; set; }
        public string SLastName { get; set; }
        public string CivilStatus { get; set; }
        public string Curp { get; set; }
        public DateTime? BirthDate { get; set; }
        public string IdBirthCountry { get; set; }
        public string IdBirthState { get; set; }
        public string Citizenship { get; set; }
        public bool? Active { get; set; }
        public DateTime modiffiedAt { get; set; } = DateTime.Now;
        public string modiffiedBy { get; set; }
    }
}
