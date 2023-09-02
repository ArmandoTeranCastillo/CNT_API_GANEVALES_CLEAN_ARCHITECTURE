using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace _1.UsersManagement.Domain.Models.Users
{
    [Table("CNT_T_USERS")]
    public class Users 
    {
        public string Id { get; set; } 
        public string User { get; set; }
        public string? Password { get; set; }
        public string? OldPassword { get; set; } 
        public DateTime PasswordExpireDate { get; set; }
        public bool PasswordExpire { get; set; }
        public bool Active { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool Autentication { get; set; }
        public string AutenticationCode { get; set; }
        public string IdUserType { get; set; }
        public string IdUserRole { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModiffiedAt { get; set; }
        public string? ModiffiedBy { get; set; }
        public bool Logged { get; set; }
        public string Curp { get; set; }
        
        
        [ForeignKey("IdUserType")]
        public virtual UserTypes UserType { get; set; }
        
        [ForeignKey("IdUserRole")]
        public virtual UserRoles UserRole { get; set; }
        public virtual string ExpireWithoutTime => PasswordExpireDate.ToShortDateString();
    }
}
