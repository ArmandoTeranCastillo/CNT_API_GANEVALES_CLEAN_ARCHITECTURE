using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace _1.UsersManagement.Domain.Models.Phones
{
    [Table("CNT_T_PHONETYPES")]
    public class PhoneTypes
    {
        public string Id { get; set; }
        public string PhoneType { get; set; }
        public string DescriptionPhoneType { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModiffiedAt { get; set; }
        public string ModiffiedBy { get; set; }
    }
}
