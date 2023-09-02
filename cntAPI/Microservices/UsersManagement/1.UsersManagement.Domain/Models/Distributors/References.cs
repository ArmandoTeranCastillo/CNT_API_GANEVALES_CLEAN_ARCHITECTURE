using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1.UsersManagement.Domain.Models.Distributors
{
    [Table("CNT_T_REFERENCES")]
    public class References
    {
        public string Id { get; set; }
        public string IdRelation { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }  
        public string FLastName { get; set; }
        public string SLastName { get; set; }
        public string id_partnership_mtz { get; set; }
        public bool Address { get; set; }
        public bool Phone { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModiffiedAt { get; set; }
        public string ModiffiedBy { get; set; }
        
        [ForeignKey("id_partnership_mtz")]
        public virtual Matrices.Matrices Matriz { get; set; }
    }
}
