using _1.UsersManagement.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1.UsersManagement.Domain.Models.Tasks
{
    [Table("CNT_T_TASKS")]
    public class Tasks 
    {
        public string Id { get; set; }
        public string IdUser { get; set; }
        public string IdTaskType { get; set; }
        public DateTime Date_Start { get; set; }
        public DateTime Date_End { get; set; }
        public string Location { get; set; }
        public int Priority { get; set; }
        public bool Finished { get; set; }
        public string IdDestination { get; set; }
        public bool Approved { get; set; }
        public string Subject { get; set; }
        public string IdStatus_mtz { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModiffiedAt { get; set; }
        public string ModiffiedBy { get; set; }
        
        
        [ForeignKey("IdTaskType")]
        public virtual TasksTypes TasksType { get; set; }
        
        [ForeignKey("IdStatus_mtz")]
        public virtual Matrices.Matrices Matrices { get; set; }
        [NotMapped] public virtual string OnlyDateStart => Date_Start.Date.ToString("yyyy-MM-dd");
        [NotMapped] public virtual string OnlyTimeStart => Date_Start.ToString("HH:mm:ss");
        [NotMapped] public virtual string OnlyDateEnd => Date_End.Date.ToString("yyyy-MM-dd");
        [NotMapped] public virtual string OnlyTimeEnd => Date_End.ToString("HH:mm:ss");
    }
}
