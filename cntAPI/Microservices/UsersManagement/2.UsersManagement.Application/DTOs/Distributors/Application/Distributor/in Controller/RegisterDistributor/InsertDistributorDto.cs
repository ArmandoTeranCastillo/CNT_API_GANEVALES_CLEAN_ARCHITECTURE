using System;

namespace _2.UsersManagement.Application.DTOs.Distributors.Application.Distributor.in_Controller.RegisterDistributor
{
    public class InsertDistributorDto
    {
        public string IdProspect { get; set; }  
        public DateTime Since { get; set; }
        public string IdSchoolarship { get; set; }
        public string Ocupation { get; set; }
        public bool Working { get; set; }
        public string CreatedBy { get; set; }
    }
}
