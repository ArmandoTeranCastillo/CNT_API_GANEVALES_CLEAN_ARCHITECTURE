using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace _1.UsersManagement.Domain.Models.Permissions
{
    [Table("CNT_T_RELATIONS")]
    public class Relations
    {
        public string Id { get; set; }
        public string IdOrigin { get; set; }
        public string IdDestination { get; set; }
        public string TableOrigin { get; set; }
        public string TableDestination { get; set; }  
        public string IdRelationType { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModiffiedAt { get; set; }
        public string ModiffiedBy { get; set; }
    }
}
