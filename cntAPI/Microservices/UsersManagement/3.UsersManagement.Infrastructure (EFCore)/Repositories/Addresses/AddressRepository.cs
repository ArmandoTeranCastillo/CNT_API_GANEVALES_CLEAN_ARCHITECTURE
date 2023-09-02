using _1.UsersManagement.Domain.Models.Addresses;
using _2.UsersManagement.Application.Interfaces.Addresses.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _3.UsersManagement.Infrastructure__EFCore_.Persistence;
using _3.UsersManagement.Infrastructure__EFCore_.Services;

namespace _3.UsersManagement.Infrastructure__EFCore_.Repositories.Addresses
{
    public class AddressRepository : GenericService<_1.UsersManagement.Domain.Models.Addresses.Addresses>, IAddressRepository
    {
        public AddressRepository(CNTContext cnt) : base(cnt) { }
    }
}
