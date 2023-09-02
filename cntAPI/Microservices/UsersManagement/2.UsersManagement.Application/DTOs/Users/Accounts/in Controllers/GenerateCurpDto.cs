using System;
using UsersManagement.CURP.Enums;

namespace _2.UsersManagement.Application.DTOs.Users.Accounts.in_Controllers
{
    public class GenerateCurpDto
    {
        public string Names { get; set; }
        public string FLastName { get; set; }
        public string SLastName { get; set; }   
        public string Gender { get; set; }
        public DateTime Birtdate { get; set; }
        public Estado State { get; set; }
    }
}
