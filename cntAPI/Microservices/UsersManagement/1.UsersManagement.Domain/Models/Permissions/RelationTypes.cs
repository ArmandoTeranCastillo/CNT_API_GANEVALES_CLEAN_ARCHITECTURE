using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace _1.UsersManagement.Domain.Models.Permissions
{
    [Table("CNT_T_RELATIONTYPES")]
    public class RelationTypes
    {
        public string Id { get; set; }
        public string RelationType { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModiffiedAt { get; set; } 
        public string ModiffiedBy { get; set; }
    }
}
