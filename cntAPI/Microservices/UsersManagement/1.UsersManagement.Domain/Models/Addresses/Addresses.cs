using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1.UsersManagement.Domain.Models.Addresses
{
    [Table("CNT_T_ADDRESSES")]
    public class Addresses
    {
        public string Id { get; set; }
        public string IdRelation { get; set; }
        public string IdZipCode { get; set; }
        public string Address { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModiffiedAt { get; set; }
        public string ModiffiedBy { get; set; }
    }
}
