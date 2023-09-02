using _1.UsersManagement.Domain.Models.Tasks;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1.UsersManagement.Domain.Interfaces;

namespace _1.UsersManagement.Domain.Models.Distributors
{
    [Table("CNT_T_AVALS")]
    public class Avals : IDistributorsEntity, ITotalIncomingEntity
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string FLastName { get; set; }
        public string SLastName { get; set; }
        public string Curp { get; set; }
        public string IdDistributor { get; set; }
        public int Dependents { get; set; }
        public int Vehicles { get; set; }
        public string Bills { get; set; }
        public string Services { get; set; }
        public string House { get; set; }
        public bool SalesXp { get; set; }
        public string TotalIncoming { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModiffiedAt { get; set; }
        public string ModiffiedBy { get; set; }
    }
}
