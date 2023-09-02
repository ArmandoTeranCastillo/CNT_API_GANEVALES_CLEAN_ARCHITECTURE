using _1.UsersManagement.Domain.Models.Phones;
using _2.UsersManagement.Application.Interfaces.Phones.Repositories;
using _3.UsersManagement.Infrastructure__EFCore_.Persistence;
using _3.UsersManagement.Infrastructure__EFCore_.Services;

namespace _3.UsersManagement.Infrastructure__EFCore_.Repositories.Phones
{
    public class PhoneLadaRepository : GenericService<PhoneLadas>, IPhoneLadaRepository
    {
        public PhoneLadaRepository(CNTContext cnt) : base(cnt) { }
    }
}