using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using _1.UsersManagement.Domain.Models.Addresses;
using _2.UsersManagement.Application.DTOs.Addresses.In_Services;

namespace _2.UsersManagement.Application.Interfaces.Addresses
{
    public interface IZipcodeService
    {
        Task<IEnumerable<string>> GetCityZipcode(string zipcode);
        Task<IEnumerable<string>> GetStateZipcode(string zipcode);
        Task<IEnumerable<string>> GetCountryZipcode(string zipcode);
        Task<GetAllAddressesDto> GetAllAddressesZipcode(Expression<Func<Zipcodes, bool>> where);
    }
}
