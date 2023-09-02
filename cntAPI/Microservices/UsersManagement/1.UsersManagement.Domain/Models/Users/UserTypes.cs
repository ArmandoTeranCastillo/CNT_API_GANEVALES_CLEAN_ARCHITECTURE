using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace _1.UsersManagement.Domain.Models.Users
{
    [Table("CNT_T_USERTYPES")]
    public class UserTypes
    {
        public string Id { get; set; }
        public string UserType { get; set; }
        public string DescriptionType { get; set; }
        public bool Enabled { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModiffiedAt { get; set; }
        public string? ModiffiedBy { get; set; }
    }
}
