using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace _1.UsersManagement.Domain.Models.Users
{
    [Table("CNT_T_USERROLES")]
    public class UserRoles
    {
        public string Id { get; set; } 
        public string UserRole { get; set; }
        public string DescriptionRole { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModiffiedAt { get; set; }
        public string? ModiffiedBy { get; set; }
    }
}
