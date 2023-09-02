using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1.UsersManagement.Domain.Models.Tasks
{
    [Table("CNT_T_TASKMESSAGES")]
    public class TaskMessages
    {
        public string Id { get; set; }
        public string IdTask { get; set; }
        public string IdUser { get; set; }
        public string Message { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModiffiedAt { get; set; }
        public string ModiffiedBy { get; set; }
        
        
        [ForeignKey("IdTask")]
        public virtual Tasks Task { get; set; }
    }
}
