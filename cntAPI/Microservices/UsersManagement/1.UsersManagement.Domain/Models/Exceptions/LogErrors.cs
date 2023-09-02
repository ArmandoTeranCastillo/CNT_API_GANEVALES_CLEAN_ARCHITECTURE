using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace _1.UsersManagement.Domain.Models.Exceptions
{
    [Table("CNT_T_LOGERROR")]
    public class LogErrors
    {
        public string Id { get; set; }
        public string Origin { get; set; }
        public string FuncionName { get; set; }
        public string CodeError { get; set; }
        public string MessageError { get; set; }
        public string Parameters { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
    }
}
