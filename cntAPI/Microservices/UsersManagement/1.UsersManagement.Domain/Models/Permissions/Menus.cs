using System;
using System.ComponentModel.DataAnnotations.Schema;
using _1.UsersManagement.Domain.Interfaces;

namespace _1.UsersManagement.Domain.Models.Permissions
{
    [Table("CNT_T_MENUS")]
    public class Menus : IEntityWithActive
    {
        public string Id { get; set; }
        public string NameMenu { get; set; }
        public string DescriptionMenu { get; set; }
        public string IconPaths { get; set; }
        public string Route { get; set; }
        public bool TypeMenu { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModiffiedAt { get; set; }
        public string ModiffiedBy { get; set; }
    }
}
