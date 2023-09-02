using System;
using System.ComponentModel.DataAnnotations.Schema;
using _1.UsersManagement.Domain.Interfaces;

namespace _1.UsersManagement.Domain.Models.Permissions
{
    [Table("CNT_T_VIEWS")]
    public class Views : IEntityWithActive
    {
        public string Id { get; set; }
        public string NameView { get; set; }
        public string DescriptionView { get; set; }
        public string Path { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModiffiedAt { get; set; }
        public string ModiffiedBy { get; set; }
    }
}
