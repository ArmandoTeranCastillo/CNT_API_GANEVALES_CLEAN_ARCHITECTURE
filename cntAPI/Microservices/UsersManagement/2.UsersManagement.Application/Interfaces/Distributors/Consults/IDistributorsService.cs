using System.Collections.Generic;
using System.Threading.Tasks;
using _1.UsersManagement.Domain.Models.Distributors;
using _1.UsersManagement.Domain.Models.Tasks;
using _2.UsersManagement.Application.DTOs.Distributors.Application.Distributor.in_Services;

namespace _2.UsersManagement.Application.Interfaces.Distributors.Consults
{
    public interface IDistributorsService
    {
        Task<IEnumerable<Prospect>> GetAllCompleteProspects();
        Task<Prospect> GetSimpleCompleteProspect(string id);
        Task<_1.UsersManagement.Domain.Models.Tasks.Tasks> UpdateTask(UpdateAppointmentDto request);
        Task<IEnumerable<_1.UsersManagement.Domain.Models.Tasks.Tasks>> GetAllAppointments();
    }
}