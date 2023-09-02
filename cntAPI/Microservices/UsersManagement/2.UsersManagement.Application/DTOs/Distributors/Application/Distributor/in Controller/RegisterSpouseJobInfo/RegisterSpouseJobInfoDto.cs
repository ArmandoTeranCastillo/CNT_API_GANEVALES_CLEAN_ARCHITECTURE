using _2.UsersManagement.Application.DTOs.Distributors.Application.Distributor.in_Controller.RegisterJobInfo;
using _2.UsersManagement.Application.DTOs.Distributors.Application.in_Controller;
using _2.UsersManagement.Application.DTOs.Distributors.Application.Shared;

namespace _2.UsersManagement.Application.DTOs.Distributors.Application.Distributor.in_Controller.RegisterSpouseJobInfo
{
    public class RegisterSpouseJobInfoDto
    {
        public InsertJobInfoDto JobInfo { get; set; }
        public InsertAddressDto JobAddress { get; set; }
    }
}