using System.ComponentModel.DataAnnotations.Schema;

namespace _1.UsersManagement.Domain.Models.Phones
{
    [Table("CNT_T_PHONELADAS")]
    public class PhoneLadas
    {
        public string Id { get; set; }
        public string PhoneLada { get; set; }
        public string City { get; set; }
        public string State { get; set; }
    }
}