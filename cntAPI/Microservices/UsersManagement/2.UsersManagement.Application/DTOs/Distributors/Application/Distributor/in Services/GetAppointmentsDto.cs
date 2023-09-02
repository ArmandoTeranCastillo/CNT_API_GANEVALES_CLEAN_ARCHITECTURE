namespace _2.UsersManagement.Application.DTOs.Distributors.Application.Distributor.in_Services
{
    public class GetAppointmentsDto
    {
        public string IdTask { get; set; }
        public string IdUser { get; set; }
        public string User { get; set; }
        public string TaskType { get; set; }
        public string DateStart { get; set; }
        public string DateEnds { get; set; }
        public string Location { get; set; }
        public int Proprity { get; set; }
        public bool Finished { get; set; }
        public string IdProspect { get; set; }
        public string ProspectName { get; set; }
        public string Subject { get; set; }
        public string Estatus { get; set; }
    }
}
