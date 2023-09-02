using System;
using _1.UsersManagement.Domain.Interfaces;

namespace _2.UsersManagement.Application.DTOs
{
    public class ActiveEntityDto : IUpdateEntity
    {
        public string id { get; set; }
        public bool? Active { get; set; }
        public DateTime modiffiedAt { get; set; } = DateTime.Now;
        public string modiffiedBy { get; set; }
    }
}