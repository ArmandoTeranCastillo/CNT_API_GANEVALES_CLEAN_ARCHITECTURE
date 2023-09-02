using System;
using _1.UsersManagement.Domain.Interfaces;

namespace _2.UsersManagement.Application.DTOs.Distributors.Application.Distributor.in_Services
{
    public class UpdateAppointmentDto : IUpdateEntity
    {
        public string id {  get; set; }
        public string IdUser { get; set; }
        public DateTime date_start { get; set; }
        public DateTime date_end { get; set; }
        public string Location { get; set; }
        public bool Finished { get; set; }
        public string idStatus_mtz { get; set; }
        public DateTime modiffiedAt { get; set; } = DateTime.Now;
        public string modiffiedBy { get; set; }
    }
}
