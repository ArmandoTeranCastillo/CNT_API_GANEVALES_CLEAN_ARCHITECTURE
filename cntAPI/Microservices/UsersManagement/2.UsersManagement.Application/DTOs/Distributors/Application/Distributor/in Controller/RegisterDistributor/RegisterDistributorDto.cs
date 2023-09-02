using _2.UsersManagement.Application.DTOs.Distributors.Application.Shared;

namespace _2.UsersManagement.Application.DTOs.Distributors.Application.Distributor.in_Controller.RegisterDistributor
{
    public class RegisterDistributorDto
    {
        public InsertDistributorDto Distributor { get; set; }
        public InsertProfileDto Profile { get; set; }   
        public InsertAddressDto Address { get; set; }
        public UpdatePhoneDto Phone { get; set; }
    }
}
