using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1.UsersManagement.Domain.Models.Distributors
{
    [Table("CNT_T_JOBINFO")]
    public class JobInfo
    {
        public string Id {  get; set; }
        public string Name { get; set; }//
        public string Title { get; set; }
        public DateTime Since { get; set; }
        public TimeSpan Checkin { get; set; }
        public TimeSpan Checkout { get; set; }
        public string Workingdays { get; set; }
        public string Responsable { get; set; }//
        public string IdRelation { get; set; }
        public string Income { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModiffiedAt { get; set; }
        public string ModiffiedBy { get; set; }
        
        public virtual string SinceWithoutTime => Since.ToShortDateString();
    }
}
