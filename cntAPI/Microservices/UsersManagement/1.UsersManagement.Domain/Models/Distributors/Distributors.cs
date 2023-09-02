using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using _1.UsersManagement.Domain.Interfaces;

namespace _1.UsersManagement.Domain.Models.Distributors
{
    [Table("CNT_T_DISTRIBUTORS")]
    public class Distributors : ITotalIncomingEntity
    {
        public string Id { get; set; }
        public int? Code { get; set; }
        public string IdUser { get; set; }
        public DateTime Since { get; set; } 
        public string id_schoolarship_mtz { get; set; }
        public string Ocupation { get; set; }
        public bool Working { get; set; }
        public string id_dvType_mtz { get; set; }
        public bool Prospect { get; set; }
        public string TotalIncoming { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModiffiedAt { get; set; }
        public string ModiffiedBy { get; set; }
        
        public virtual string SinceWithoutTime => Since.ToShortDateString();
    }
}
