using System.Collections.Generic;
using _2.UsersManagement.Application.DTOs.Distributors.Application.Distributor.in_Controller.RegisterVehicle;

namespace _2.UsersManagement.Application.DTOs.Distributors.Application.in_Controller.RegisterVehicle
{
    public class RegisterVehicleDto
    {
        public List<InsertVehicleDto> Vehicles { get; set; }
    }
}