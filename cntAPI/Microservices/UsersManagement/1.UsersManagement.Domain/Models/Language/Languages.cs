using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace _1.UsersManagement.Domain.Models.Language
{
    [Table("CNT_T_LANGUAGES")]
    public class Languages
    {
        public string Id { get; set; }
        public string IdControl { get; set; }
        public string es_mx { get; set; }
        public string en_us { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModiffiedAt { get; set; }
        public string ModiffiedBy { get; set; }
    }
}