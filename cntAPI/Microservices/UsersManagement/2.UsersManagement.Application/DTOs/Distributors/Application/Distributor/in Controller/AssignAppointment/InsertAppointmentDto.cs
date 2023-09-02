using System;

namespace _2.UsersManagement.Application.DTOs.Distributors.Application.Distributor.in_Controller.AssignAppointment
{
    public class InsertAppointmentDto
    {
        public string IdUser { get; set; }
        public DateTime date_start { get; set; }
        public DateTime date_end { get; set; }
        public string Location { get; set; }
        public int Priority { get; set; }
        public string Subject { get; set; }
        public string idStatus_mtz { get; set; }
        public string CreatedBy { get; set; }
    }
}
