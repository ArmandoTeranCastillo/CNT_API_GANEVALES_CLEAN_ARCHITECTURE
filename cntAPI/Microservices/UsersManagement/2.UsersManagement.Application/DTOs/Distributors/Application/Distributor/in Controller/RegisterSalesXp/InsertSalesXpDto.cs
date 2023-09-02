using System;

namespace _2.UsersManagement.Application.DTOs.Distributors.Application.Distributor.in_Controller.RegisterSalesXp
{
    public class InsertSalesXpDto
    {
        public string IdDistributor { get; set; }
        public string company_name { get; set; }
        public string Limit { get; set; }
        public DateTime Since { get; set; }
        public string Comission { get; set; }
        public string Other { get; set; }
        public string CreatedBy { get; set; }
    }
}
