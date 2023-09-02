using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1.UsersManagement.Domain.Models.Distributors
{
    [Table("CNT_T_SALESXP")]
    public class SalesXp
    {
        public string Id { get; set; }
        public string IdDistributor { get; set; }
        public string company_name { get; set; }
        public string Limit { get; set; }
        public DateTime Since { get; set; }
        public string Comission { get; set; }
        public string Other { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModiffiedAt { get; set; }
        public string ModiffiedBy { get; set; }
    }
}
