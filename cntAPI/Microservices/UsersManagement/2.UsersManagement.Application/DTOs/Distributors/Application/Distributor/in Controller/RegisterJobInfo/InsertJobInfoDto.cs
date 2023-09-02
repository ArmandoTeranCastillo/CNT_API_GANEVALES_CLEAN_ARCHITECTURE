using System;
using System.Text.Json.Serialization;

namespace _2.UsersManagement.Application.DTOs.Distributors.Application.Distributor.in_Controller.RegisterJobInfo
{
    public class InsertJobInfoDto
    {
        public string Name {  get; set; }
        public string Title { get; set; }
        public DateTime Since { get; set; }
        public string Checkin { get; set; }
        public string Checkout { get; set; }
        public string Workingdays { get; set; }
        public string Responsable { get; set; }
        public string IdRelation { get; set; }
        public string Income { get; set; }
        public string CreatedBy { get; set; }
        [JsonIgnore]
        public virtual TimeSpan CheckinTime => TimeSpan.Parse(Checkin);
        [JsonIgnore]
        public virtual TimeSpan CheckoutTime => TimeSpan.Parse(Checkout);
    }

}
