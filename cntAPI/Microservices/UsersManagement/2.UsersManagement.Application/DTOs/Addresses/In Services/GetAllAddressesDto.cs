using System.Collections.Generic;

namespace _2.UsersManagement.Application.DTOs.Addresses.In_Services
{
    public class GetAllAddressesDto
    {
        public string Id { get; set; }
        public string IdCountry { get; set; }
        public string Country { get; set; }
        public string IdState { get; set; }
        public string State { get; set; }
        public string IdMunicipality { get; set; }
        public string Municipality { get; set; }
        public string IdCity { get; set; }
        public string City { get; set; }
        public string Zipcode { get; set; }
        public string Street { get; set; }
        public List<CommunityDto> Communities { get; set; }
    }
}
