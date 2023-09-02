using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace _1.UsersManagement.Domain.Models.Addresses
{
    [Table("CNT_T_ZIPCODES")]
    public class Zipcodes
    {
        public string Id { get; set; }
        public string CommunityName { get; set; }
        public string CommunityType { get; set; }
        public string ZipCode { get; set; }
        public string IdCity { get; set; }
        public bool Active { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModiffiedAt { get; set; }
        public string ModiffiedBy { get; set; }
        
        
        [ForeignKey("CNT_T_CITIES")]
        public virtual Cities City { get; set; }
    }
}
