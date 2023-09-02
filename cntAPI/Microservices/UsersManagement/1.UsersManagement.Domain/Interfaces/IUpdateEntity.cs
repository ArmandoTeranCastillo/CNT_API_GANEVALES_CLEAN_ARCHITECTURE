using System;

namespace _1.UsersManagement.Domain.Interfaces
{
    public interface IUpdateEntity
    {
        public string id { get; set; }
        public DateTime modiffiedAt { get; set; } 
        public string modiffiedBy { get; set; }
    }
}
