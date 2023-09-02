using System;

namespace _2.UsersManagement.Application.DTOs.Tasks
{
    public class InsertTaskDto 
    {
        public DateTime date_start { get; set; }
        public DateTime date_end { get; set; }
        public string IdUser { get; set; }
        public string IdDestination { get; set; }
        public string Location { get; set; }
        public string Subject { get; set; }
        public int Priority { get; set; } = 0;
        public string idStatus_mtz { get; set; }
        public string CreatedBy { get; set; }
    }
}