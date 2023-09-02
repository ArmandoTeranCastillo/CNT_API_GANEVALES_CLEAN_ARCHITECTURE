using System.ComponentModel.DataAnnotations.Schema;
using System;
using _1.UsersManagement.Domain.Interfaces;
using System.Collections.Generic;
using System.ComponentModel;

namespace _1.UsersManagement.Domain.Models.Addresses
{
    [Table("CNT_T_CITIES")]
    public class Cities 
    {
        public string Id { get; set; }
        public string CityName { get; set; }
        public string CityCode { get; set; }
        public string IdMunicipality { get; set; }
        public bool Active { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModiffiedAt { get; set; }
        public string ModiffiedBy { get; set; }
        
        [ForeignKey("IdMunicipality")]
        public virtual Municipalities Municipality { get; set; }
    }
}

