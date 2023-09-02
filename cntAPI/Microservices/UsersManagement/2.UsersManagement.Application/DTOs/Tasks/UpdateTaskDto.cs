using System;
using _1.UsersManagement.Domain.Interfaces;

namespace _2.UsersManagement.Application.DTOs.Tasks
{
    public class UpdateTaskDto : IUpdateEntity
    {
        public string id { get; set; }
        public DateTime? date_start { get; set; }
        public DateTime? date_end { get; set; }
        public string Location { get; set; }
        public string Subject { get; set; }
        public int? Priority { get; set; } = 0;
        public string idStatus_mtz { get; set; }
        public bool? Active { get; set; }
        public DateTime modiffiedAt { get; set; } = DateTime.Now;
        public string modiffiedBy { get; set; }
    }
}