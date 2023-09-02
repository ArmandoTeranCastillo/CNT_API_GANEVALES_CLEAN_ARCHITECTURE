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
    public class ProspectRepository : GenericService<Prospect>, IProspectRepository
    {
        public ProspectRepository(CNTContext cnt) : base(cnt) { }
    }
}