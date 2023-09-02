using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace _1.UsersManagement.Domain.Models.Addresses
{
    [Table("CNT_T_COUNTRYCODE")]
    public class Countries
    {
        public string Id { get; set; }
        public char Iso { get; set; }
        public string Name { get; set; }
        public string Nicename { get; set; }
        public string Iso3 { get; set; }
        public int? Numcode { get; set; }
        public int Phonecode { get; set; }
        public byte? Flag { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModiffiedAt { get; set; }
        public string ModiffiedBy { get; set; }
    }
}
