using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace _1.UsersManagement.Domain.Models.Documents
{
    [Table("CNT_T_DOCTOUSERS")]
    public class DoctoUsers
    {
        public string Id { get; set; }
        public string IdUser { get; set; }
        public string IdDoctoType { get; set; }
        public string DoctoName { get; set; }
        public string DoctRoute { get; set; }
        public string DocUrls { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModiffiedAt { get; set; }
        public string ModiffiedBy { get; set; }
    }
}