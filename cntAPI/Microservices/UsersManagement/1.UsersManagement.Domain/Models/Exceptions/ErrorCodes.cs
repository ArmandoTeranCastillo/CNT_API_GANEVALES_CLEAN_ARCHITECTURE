using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace _1.UsersManagement.Domain.Models.Exceptions
{
    [Table("CNT_T_ERRORCODES")]
    public class ErrorCodes
    {
        public string Id { get; set; }
        public string CodeError { get; set; }
        public string DescriptionError { get; set; }
        public string Help { get; set; }
        public string IdDescriptionError { get; set; }
        public string IdHelpError { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModiffiedAt { get; set; }
        public string ModiffiedBy { get; set; }
    }
}
