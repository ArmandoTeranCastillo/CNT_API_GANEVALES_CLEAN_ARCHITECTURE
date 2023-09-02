using _1.UsersManagement.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace _2.UsersManagement.Application.DTOs.Users.Accounts.In_Services
{
    public class UpdateAddressDto : IUpdateEntity
    {
        public string id { get; set; }
        public string IdRelation {  get; set; }
        public string IdZipCode { get; set; }
        public string Address { get; set; }
        public DateTime modiffiedAt { get; set; } = DateTime.Now;
        public string modiffiedBy { get; set; }
    }
}
