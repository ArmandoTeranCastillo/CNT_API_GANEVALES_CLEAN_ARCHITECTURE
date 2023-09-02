using System;

namespace _1.Web.Gateway.Domain.Models
{
    public abstract class CNT_T_USERS
    {
        public string id { get; set; }
        public string user { get; set; }
        public string? password { get; set; }
        public string? oldPassword { get; set; }
        public DateTime passwordExpireDate { get; set; }
        public bool passwordExpire { get; set; }
        public bool active { get; set; }
        public string email { get; set; }
        public string phoneNumber { get; set; }
        public bool autentication { get; set; }
        public string autenticationCode { get; set; }
        public string idUserType { get; set; }
        public string idUserRole { get; set; }
        public DateTime createdAt { get; set; }
        public string createdBy { get; set; }
        public DateTime modiffiedAt { get; set; }
        public string? modiffiedBy { get; set; }
        public bool logged { get; set; }
    }
}
