using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1.UsersManagement.Domain.Models.Distributors
{
    [Table("CNT_T_VEHICLES")]
    public class Vehicles
    {
        public string Id { get; set; }
        public string IdRelation { get; set; }
        public string Brand { get; set; }
        public string Serie { get; set; }
        public string Model { get; set; }
        public string Price { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModiffiedAt { get; set; }
        public string ModiffiedBy { get; set; }
    }
}
