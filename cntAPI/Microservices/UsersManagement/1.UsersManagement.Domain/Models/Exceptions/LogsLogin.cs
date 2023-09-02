using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace _1.UsersManagement.Domain.Models.Exceptions
{
    [Table("CNT_T_LOGS_LOGIN")]
    public class LogsLogin
    {
        public string Id { get; set; }
        public string IdUser { get; set; }
        public DateTime FirtsLogin { get; set; }
        public DateTime SecondLogin { get; set; }
        public DateTime EndLogin { get; set; }
        public DateTime LastActivity { get; set; }
        public DateTime TimeActive { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModiffiedAt { get; set; }
        public string ModiffiedBy { get; set; }
    }
}
