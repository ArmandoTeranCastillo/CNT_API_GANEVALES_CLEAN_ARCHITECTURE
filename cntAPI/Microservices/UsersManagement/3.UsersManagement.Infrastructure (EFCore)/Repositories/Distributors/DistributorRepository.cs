﻿using _1.UsersManagement.Domain.Models.Distributors;
using _2.UsersManagement.Application.Interfaces.Distributors.Consults.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _3.UsersManagement.Infrastructure__EFCore_.Persistence;
using _3.UsersManagement.Infrastructure__EFCore_.Services;

namespace _3.UsersManagement.Infrastructure__EFCore_.Repositories.Distributors
{
    public class DistributorRepository : GenericService<_1.UsersManagement.Domain.Models.Distributors.Distributors>, IDistributorRepository
    {
        public DistributorRepository(CNTContext cnt) : base(cnt) { }
    }
}
