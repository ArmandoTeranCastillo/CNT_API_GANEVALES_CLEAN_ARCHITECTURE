using System;
using _1.UsersManagement.Domain.Interfaces;

namespace _2.UsersManagement.Application.DTOs.Distributors.Application.Distributor.in_Controller.RegisterDependent
{
    public class InsertDependentDto : IRelationEntity
    {
        public string idRelation { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string FLastName { get; set; }
        public string SLastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string id_dependent_mtz { get; set; }
        public string Ocupation { get; set; }
        public string Income { get; set; }
        public string CreatedBy { get; set; }
    }
}
