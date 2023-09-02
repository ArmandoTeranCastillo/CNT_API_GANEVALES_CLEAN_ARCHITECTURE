using System;
using System.ComponentModel.DataAnnotations.Schema;
using _1.UsersManagement.Domain.Interfaces;

namespace _1.UsersManagement.Domain.Models.Permissions
{
    [Table("CNT_T_CONTROLS")]
    public class Controls : IEntityWithActive
    {
        public string Id { get; set; }
        public string NameControl { get; set; }
        public string DescriptionControl { get; set; }
        public string Route { get; set; }
        public string RouteIcon { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModiffiedAt { get; set; }
        public string Modiffiedby { get; set; }
    }
}
