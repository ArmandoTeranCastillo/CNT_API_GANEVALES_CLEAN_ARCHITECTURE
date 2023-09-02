using System;
using System.ComponentModel.DataAnnotations.Schema;
using _1.UsersManagement.Domain.Models.Addresses;

namespace _1.UsersManagement.Domain.Models.Users
{
    [Table("CNT_T_PROFILES")]
    public class Profiles 
    {
        public string Id { get; set; }
        public string IdUser { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string FLastName { get; set; }
        public string SLastName { get; set; }
        public string CivilStatus { get; set; }
        public string Gender { get; set; }
        public string Curp { get; set; }
        public string IdDocNumber { get; set; }
        public string IdDocType { get; set; }
        public DateTime BirthDate { get; set; }
        public string IdBirthCountry { get; set; }
        public string IdBirthState { get; set; }
        public string IdBirthCity { get; set; }
        public string Citizenship { get; set; }
        public string ProfileImage { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModiffiedAt { get; set; }
        public string ModiffiedBy { get; set; }
        
        
        
        [ForeignKey("IdUser")]
        public virtual Users User { get; set; }
        
        [ForeignKey("IdBirthCountry")]
        public virtual Countries Country { get; set; }
        
        [ForeignKey("IdBirthState")]
        public virtual States State { get; set; }
        
        [ForeignKey("IdBirthCity")]
        public virtual Cities City { get; set; }
        public virtual string BirthWithoutTime => BirthDate.ToShortDateString();
    }
}
