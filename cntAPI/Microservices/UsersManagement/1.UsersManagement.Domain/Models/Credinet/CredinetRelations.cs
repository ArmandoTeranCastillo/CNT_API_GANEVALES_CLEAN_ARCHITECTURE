using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1.UsersManagement.Domain.Models.Credinet
{
    [Table("CNT_T_CREDINETRELATIONS")]
    public class CredinetRelations
    {
        public string Id {  get; set; }
        public string IdCnt { get; set; }
        public string IdCredinet { get; set; }
        public string TableCnt { get; set; }
        public string TableCredinet { get; set; }
        public string IdRelationType { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModiffiedAt { get; set; }
        public string ModiffiedBy { get; set; }
    }
}
