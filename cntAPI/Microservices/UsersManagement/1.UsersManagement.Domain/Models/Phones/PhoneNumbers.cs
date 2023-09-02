using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace _1.UsersManagement.Domain.Models.Phones
{
    [Table("CNT_T_PHONENUMBERS")]
    public class PhoneNumbers
    {
        public string Id { get; set; }
        public string IdRelation { get; set; }
        public string IdPhoneType { get; set; }
        public string IdCountryCode { get; set; }
        public string CityPhoneCode { get; set; }
        public string PhoneNumber { get; set; }
        
        public string PhoneExt { get; set; }
        public bool Active { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModiffiedAt { get; set; }
        public string ModiffiedBy { get; set; }
    }
}
