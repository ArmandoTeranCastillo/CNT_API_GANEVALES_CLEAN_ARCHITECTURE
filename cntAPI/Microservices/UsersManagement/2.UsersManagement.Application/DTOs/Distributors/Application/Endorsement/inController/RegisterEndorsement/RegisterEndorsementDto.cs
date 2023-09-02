using _2.UsersManagement.Application.DTOs.Distributors.Application.Distributor.in_Controller.RegisterDistributor;
using _2.UsersManagement.Application.DTOs.Distributors.Application.Shared;

namespace _2.UsersManagement.Application.DTOs.Distributors.Application.Endorsement.inController.RegisterEndorsement
{
    public class RegisterEndorsementDto
    {
        public InsertEndorsementDto Endorsement { get; set; }
        public InsertProfileDto Profile { get; set; }
        public InsertAddressDto Address { get; set; }
        public InsertPhoneDto Phone { get; set; }
    }
}