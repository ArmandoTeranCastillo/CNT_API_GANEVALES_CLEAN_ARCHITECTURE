using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace _1.Web.Gateway.Domain.Models
{
    public abstract class CNT_T_PROFILES
    {
        public string id { get; set; }
        [NotMapped]
        public CNT_T_USERS CNT_T_USERS { get; set; }
        [ForeignKey("CNT_T_USERS")]
        public string idUser { get; set; }
        public string firstName { get; set; }
        public string middleName { get; set; }
        public string fLastName { get; set; }
        public string sLastName { get; set; }
        public string civilStatus { get; set; }
        public string gender { get; set; }
        public string curp { get; set; }
        public string idDocNumber { get; set; }
        public string docType { get; set; }
        public DateTime birthDate { get; set; }
        public string idBirthCountry { get; set; } = string.Empty;
        public string idBirthState { get; set; }
        public string idBirthCity { get; set; } = string.Empty;
        public string citizenship { get; set; }
        public string profileImage { get; set; }
        public bool active { get; set; }
        public DateTime createdAt { get; set; }
        public string createdBy { get; set; }
        public DateTime modiffiedAt { get; set; }
        public string modiffiedBy { get; set; }

    }
}
