using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace _1.UsersManagement.Domain.Models.Addresses
{
    [Table("CNT_T_MUNICIPALITIES")]
    public class Municipalities
    {
        public string Id { get; set; }
        public string MunicipalityName { get; set; }
        public string IdState { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModiffiedAt { get; set; }
        public string ModiffiedBy { get; set; }
        
        
        [NotMapped]
        public States State { get; set; }
    }
}
