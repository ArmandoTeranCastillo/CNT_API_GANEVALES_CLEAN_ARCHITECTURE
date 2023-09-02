using _2.UsersManagement.Application.DTOs.Distributors.Application.Shared;

namespace _2.UsersManagement.Application.DTOs.Distributors.Application.Distributor.in_Controller.RegisterProspect
{
    public class RegisterProspectDto
    {
        public InsertProspectDto Prospect { get; set; }
        public InsertPhoneDto Phone { get; set; }
    }
}
