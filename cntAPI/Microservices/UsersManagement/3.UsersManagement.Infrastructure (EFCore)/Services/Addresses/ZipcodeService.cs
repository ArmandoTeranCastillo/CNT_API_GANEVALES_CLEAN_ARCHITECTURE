using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using _1.UsersManagement.Domain.Models.Addresses;
using _2.UsersManagement.Application.DTOs.Addresses.In_Services;
using _2.UsersManagement.Application.Interfaces.Addresses;
using _2.UsersManagement.Application.Transients;
using _3.UsersManagement.Infrastructure__EFCore_.Persistence;
using Microsoft.EntityFrameworkCore;
using UsersManagement.Common.Errors;
using UsersManagement.Common.Utilities;

namespace _3.UsersManagement.Infrastructure__EFCore_.Services.Addresses
{
    public class ZipcodeService : IZipcodeService
    {
        private readonly CNTContext _cnt;
        private readonly IGenericUnit _gUnit;

        public ZipcodeService(CNTContext cnt, IGenericUnit gUnit)
        {
            _cnt = cnt;
            _gUnit = gUnit;
        }

        public async Task<IEnumerable<string>> GetCityZipcode(string zipcode)
        {
            var result = await _cnt.Zipcodes
                .Where(i => i.ZipCode == zipcode)
                .Join(_cnt.Cities,
                    zip => zip.IdCity,
                    city => city.Id,
                    (zip, city) => city.CityName)
                .Distinct()
                .ToListAsync();
            if (!result.Any()) throw new NotFoundException(Codes.EmptyField);

            return result;
        }
        
        public async Task<IEnumerable<string>> GetStateZipcode(string zipcode)
        {
            var result = await _cnt.Zipcodes
                .Where(i => i.ZipCode == zipcode)
                .Join(_cnt.Cities,
                          zip => zip.IdCity,
                          city => city.Id,
                          (zip, city) => new { idMunicipality = city.IdMunicipality })
                .Join(_cnt.Municipalities,
                          city => city.idMunicipality,
                          mun => mun.Id,
                          (city, mun) => new { idState = mun.IdState })
                .Join(_cnt.States,
                          mun => mun.idState,
                          state => state.Id,
                          (mun, state) => state.StateName)
                .Distinct()
                .ToListAsync();
            if (!result.Any()) throw new NotFoundException(Codes.EmptyField);

            return result;
        }

        public async Task<IEnumerable<string>> GetCountryZipcode(string zipcode)
        {
            var result = await _cnt.Zipcodes
                .Where(i => i.ZipCode == zipcode)
                .Join(_cnt.Cities,
                          zip => zip.IdCity,
                          city => city.Id,
                          (zip, city) => new { idMunicipality = city.IdMunicipality })
                .Join(_cnt.Municipalities,
                          city => city.idMunicipality,
                          mun => mun.Id,
                          (city, mun) => new { idState = mun.IdState })
                .Join(_cnt.States,
                          mun => mun.idState,
                          state => state.Id,
                          (mun, state) => new { idCountry = state.IdCountry })
                .Join(_cnt.Countries,
                          state => state.idCountry,
                          country => country.Id,
                          (state, country) => country.Name)
                .Distinct()
                .ToListAsync();
            if (!result.Any()) throw new NotFoundException(Codes.EmptyField);

            return result;
        }

        public async Task<GetAllAddressesDto> GetAllAddressesZipcode(Expression<Func<Zipcodes, bool>> where)
        {
            string idAddress;
            string street;
            var result = await _cnt.Zipcodes
                .Where(where)
                .Join(_cnt.Cities,
                        zip => zip.IdCity,
                        city => city.Id,
                        (zip, city) => new {
                            id = zip.Id,
                            zipCode = zip.ZipCode, idCity = city.Id, CityName = city.CityName,
                            communityName = zip.CommunityName,
                            idMunicipality = city.IdMunicipality })
                .Join(_cnt.Municipalities,
                        city => city.idMunicipality,
                        mun => mun.Id,
                        (city, mun) => new { city.id, city.zipCode, city.idCity, city.CityName, idMunicipality = mun.Id, MunicipalityName = mun.MunicipalityName, city.communityName,
                            idState = mun.IdState })
                .Join(_cnt.States,
                        mun => mun.idState,
                        state => state.Id,
                        (mun, state) => new { mun.id, mun.zipCode, mun.idCity, mun.CityName, mun.idMunicipality, mun.MunicipalityName, idState = state.Id, StateName = state.StateName, mun.communityName,
                            idCountry = state.IdCountry })
                .Join(_cnt.Countries,
                        state => state.idCountry,
                        country => country.Id,
                        (state, country) => new { state.id, state.zipCode, state.idCity, state.CityName, state.idMunicipality, state.MunicipalityName, state.idState, state.StateName, state.communityName, idCountry = country.Id, CountryName = country.Name })
                .ToListAsync();
            if (!result.Any()) throw new NotFoundException(Codes.ZipcodeNotValid);

            try 
            {
                var id = result.FirstOrDefault()?.id;
                var address = await _gUnit.Address.GetSimpleById(i => i.IdZipCode == id);
                idAddress = address.Id;
                street = address.Address;
            }
            catch (Exception)
            {
                idAddress = null;
                street = null;
            }
                
            return new GetAllAddressesDto
            {
                Id = idAddress,
                IdCountry = result.FirstOrDefault()?.idCountry,
                Country = result.FirstOrDefault()?.CountryName,
                IdState = result.FirstOrDefault()?.idState,
                State = result.FirstOrDefault()?.StateName,
                IdMunicipality = result.FirstOrDefault()?.idMunicipality,
                Municipality = result.FirstOrDefault()?.MunicipalityName,
                IdCity = result.FirstOrDefault()?.idCity,
                City = result.FirstOrDefault()?.CityName,
                Zipcode = result.FirstOrDefault()?.zipCode,
                Street = street,
                Communities = result.Select(r => new CommunityDto { id = r.id, CommunityName = r.communityName }).Distinct().ToList(),
            };
        }
    }
}

