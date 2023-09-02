using System;

namespace _2.UsersManagement.Application.DTOs.Distributors.Application.Distributor.in_Controller.RegisterSpouse
{
    public class InsertSpouseDto
    {
        public string IdRelation { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string FLastName { get; set; }
        public string SLastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Ocupation { get; set; }
        public string Curp { get; set; }
        public string CreatedBy { get; set; }
    }
}
