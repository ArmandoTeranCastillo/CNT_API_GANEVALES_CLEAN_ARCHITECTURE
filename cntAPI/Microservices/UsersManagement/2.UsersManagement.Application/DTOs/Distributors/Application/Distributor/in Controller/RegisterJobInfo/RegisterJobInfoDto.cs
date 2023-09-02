using _2.UsersManagement.Application.DTOs.Distributors.Application.in_Controller;
using _2.UsersManagement.Application.DTOs.Distributors.Application.Shared;

namespace _2.UsersManagement.Application.DTOs.Distributors.Application.Distributor.in_Controller.RegisterJobInfo
{
    public class RegisterJobInfoDto
    {
        public InsertJobInfoDto JobInfo { get; set; }
        public InsertAddressDto Address { get; set; }
    }
}