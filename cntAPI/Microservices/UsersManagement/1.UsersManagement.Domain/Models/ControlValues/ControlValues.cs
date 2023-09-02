using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1.UsersManagement.Domain.Models.ControlValues
{
    [Table("CNT_T_CONTROLVALUES")]
    public class ControlValues
    {
        public string Id {  get; set; }
        public string ControlName { get; set; }
        public string ControlDescription { get; set; }
        public string ControlValue { get; set; }
        public string ControlOption1 { get; set; }
        public string ControlOption2 { get; set; }
        public string ControlOption3 { get; set; }
        public string ControlOption4 { get; set; }
        public string ControlOption5 { get; set; }
        public string ControlOption6 { get; set; }
        public string ControlOption7 { get; set; }
        public string ControlOption8 { get; set; }
        public string ControlOption9 { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModiffiedAt { get; set; }
        public string ModiffiedBy { get; set; }
    }
}
