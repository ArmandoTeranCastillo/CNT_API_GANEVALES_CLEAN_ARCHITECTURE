using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace _1.UsersManagement.Domain.Models.Addresses
{
    [Table("CNT_T_STATES")]
    public class States
    {
        public string Id { get; set; }
        public string StateName { get; set; }
        public string IdCountry { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModiffiedAt { get; set; }
        public string ModiffiedBy { get; set; }

        [NotMapped]
        public Countries Country { get; set; }
    }
}
