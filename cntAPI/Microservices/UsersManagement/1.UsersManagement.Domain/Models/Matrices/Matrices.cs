using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace _1.UsersManagement.Domain.Models.Matrices
{
    [Table("CNT_T_MATRIZ")]
    public class Matrices
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Order { get; set; }
        public string id_matriz_type { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModiffiedAt { get; set; }
        public string ModiffiedBy { get; set; }
        
        
        [ForeignKey("id_matriz_type")]
        public virtual MatrizType MatrizType { get; set; }
    }
}
