using _1.UsersManagement.Domain.Models.Tasks;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using _1.UsersManagement.Domain.Interfaces;
using _1.UsersManagement.Domain.Models.Phones;

namespace _1.UsersManagement.Domain.Models.Distributors
{
    [Table("CNT_T_PROSPECT")]
    public class Prospect : IDistributorsEntity
    {
        public string Id {  get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }  
        public string FLastName { get; set; }
        public string SLastName { get; set; }
        public string Curp { get; set; }
        public string IdUser { get; set; }
        public string IdTask { get; set; }
        public string IdDistributor { get; set; }
        public int Dependents { get; set; }
        public int Vehicles { get; set; }
        public string Bills { get; set; }
        public string Services { get; set; }
        public string House { get; set; }
        public bool SalesXp { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModiffiedAt { get; set; }
        public string ModiffiedBy { get; set; }
        
        [NotMapped]
        public virtual Tasks.Tasks Task { get; set; }
        [NotMapped]
        public virtual PhoneNumbers Phone { get; set; }
        [NotMapped]
        public virtual JobInfo JobInfo { get; set; }
        [NotMapped] 
        public virtual List<Vehicles> LVehicles { get; set; }
        [NotMapped]
        public virtual List<Dependents> LDependents { get; set; }
        [NotMapped]
        public virtual List<SalesXp> LSalesXp { get; set; }
        [NotMapped]
        public virtual Spouse Spouse { get; set; }
        [NotMapped]
        public virtual JobInfo SpouseJob { get; set; }
    }
}
