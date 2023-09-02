using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace _1.UsersManagement.Domain.Models.Documents
{
    [Table("CNT_T_DOCTOTYPES")]
    public class DoctoTypes
    {
        public string Id { get; set; }
        public string DoctoType { get; set; }
        public string Desc { get; set; }
        public string IdGroupDocsMtz { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModiffiedAt { get; set; }
        public string ModiffiedBy { get; set; }
        
        
        [ForeignKey("IdGroupDocsMtz")]
        public virtual Matrices.Matrices Matrices { get; set; }
    }
}