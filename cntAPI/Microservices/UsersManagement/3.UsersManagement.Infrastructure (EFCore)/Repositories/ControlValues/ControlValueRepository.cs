using _1.UsersManagement.Domain.Models.ControlValues;
using _2.UsersManagement.Application.Interfaces.ControlValues.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _3.UsersManagement.Infrastructure__EFCore_.Persistence;
using _3.UsersManagement.Infrastructure__EFCore_.Services;

namespace _3.UsersManagement.Infrastructure__EFCore_.Repositories.ControlValues
{
    public class ControlValueRepository : GenericService<_1.UsersManagement.Domain.Models.ControlValues.ControlValues>, IControlValueRepository
    {
        public ControlValueRepository(CNTContext cnt) : base(cnt) { }
    }
}
