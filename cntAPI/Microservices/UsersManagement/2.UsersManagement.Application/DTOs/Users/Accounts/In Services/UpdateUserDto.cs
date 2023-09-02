using System;
using _1.UsersManagement.Domain.Interfaces;

namespace _2.UsersManagement.Application.DTOs.Users.Accounts.In_Services
{
    public class UpdateUserDto : IUpdateEntity
    {
        public string id { get; set; }
        public string User { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Curp { get; set; }
        public bool? Active { get; set; }
        public DateTime modiffiedAt { get; set; } = DateTime.Now;
        public string modiffiedBy { get; set; }
    }
}
