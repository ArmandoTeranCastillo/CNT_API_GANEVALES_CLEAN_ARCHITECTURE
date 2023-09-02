using _1.UsersManagement.Domain.Interfaces;

namespace _2.UsersManagement.Application.DTOs.Distributors.Application.Distributor.in_Controller.RegisterVehicle
{
    public class InsertVehicleDto : IRelationEntity
    {
        public string idRelation { get; set; }
        public string Brand { get; set; }
        public string Serie { get; set; }
        public string Model { get; set; }
        public string Price { get; set; }
        public string CreatedBy { get; set; }
    }
}
