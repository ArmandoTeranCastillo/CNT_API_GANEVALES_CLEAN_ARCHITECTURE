using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1.UsersManagement.Domain.Models.Distributors
{
    [Table("CNT_T_DEPENDENTS")]
    public class Dependents
    {
        public string Id { get; set; }
        public string IdRelation { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string FLastName { get; set; }
        public string SLastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string id_dependent_mtz { get; set; }
        public string Ocupation { get; set; }
        public string Income { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModiffiedAt { get; set; }
        public string ModiffiedBy { get; set; }
        
        [ForeignKey("id_dependent_mtz")]
        public virtual Matrices.Matrices Matriz { get; set; }
        public virtual string BirthWithoutTime => BirthDate.ToShortDateString();
    }
}
