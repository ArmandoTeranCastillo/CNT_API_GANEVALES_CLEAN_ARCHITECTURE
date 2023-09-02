using _2.UsersManagement.Application.Interfaces;
using System.Threading.Tasks;
using _2.UsersManagement.Application.DTOs.Addresses.In_Services;
using _2.UsersManagement.Application.DTOs.Users.Accounts.in_Controllers;
using _2.UsersManagement.Application.Transients;
using UsersManagement.Common.Errors;
using UsersManagement.Common.Utilities;
using UsersManagement.CURP;
using UsersManagement.CURP.Enums;

namespace _3.UsersManagement.Infrastructure__EFCore_.Services
{
    public class CurpService : ICurpService
    {
        private readonly IGenericUnit _gUnit;
        public CurpService(IGenericUnit gUnit) 
        {
            _gUnit = gUnit;
        }
        public string CreateCurp(GenerateCurpDto request)
        {
            if (request.Gender.Length != 1 || !"HM".Contains(request.Gender.ToUpper()))
            {
                throw new BadRequestException(Codes.FailedCurp);
            }

            var sexEnum = (Sexo)request.Gender[0];

            return Curp.Generar(request.Names, request.FLastName, request.SLastName, sexEnum, request.Birtdate, request.State);
        }

        public async Task<int> GetStateEnum(string idBirthState)
        {
            await _gUnit.State.EntityExists(idBirthState, "id");

            var state = await _gUnit.State
                            .GetSimpleOneFieldById(i => new GetOneFieldDto { Id = i.Id, Field = i.StateName }, i => i.Id == idBirthState);
            var stateEnum = _gUnit.Country.GetEnumByName<Estado>(state.Field);
            return stateEnum;
        }
    }
}
