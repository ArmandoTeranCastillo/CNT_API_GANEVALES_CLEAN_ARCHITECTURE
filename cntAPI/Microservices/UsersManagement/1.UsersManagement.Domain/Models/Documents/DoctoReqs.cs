using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace _1.UsersManagement.Domain.Models.Documents
{
    [Table("CNT_T_DOCTOREQS")]
    public class DoctoReqs
    {
        public string Id { get; set; }
        public string IdDoctoType { get; set; }
        public string Required { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModffiedAt { get; set; }
        public string ModiffiedBy { get; set; }
    }
}