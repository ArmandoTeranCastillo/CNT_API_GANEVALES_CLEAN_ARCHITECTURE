using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _1.UsersManagement.Domain.Models.Distributors;
using _1.UsersManagement.Domain.Models.Tasks;
using _2.UsersManagement.Application.DTOs.Distributors.Application.Distributor.in_Services;
using _2.UsersManagement.Application.Interfaces.Distributors.Consults;
using _2.UsersManagement.Application.Transients;
using _3.UsersManagement.Infrastructure__EFCore_.External;
using _3.UsersManagement.Infrastructure__EFCore_.Persistence;
using Microsoft.EntityFrameworkCore;
using UsersManagement.Common.Utilities;

namespace _3.UsersManagement.Infrastructure__EFCore_.Services.Distributors.Consults
{
    public class DistributorsService : IDistributorsService
    {
        private readonly IGenericUnit _gUnit;
        private readonly CNTContext _cnt;

        public DistributorsService(CNTContext cnt, IGenericUnit gUnit)
        {
            _cnt = cnt;
            _gUnit = gUnit;
        }

        public async Task<IEnumerable<Prospect>> GetAllCompleteProspects()
        {
            var prospects = await _gUnit.Prospect
                .GetAllById(i => i.FirstName, i => string.IsNullOrEmpty(i.IdDistributor));
            var phoneNumbers = await _gUnit.PhoneNumber.GetAll();

            foreach (var prospect in prospects)
            {
                prospect.Phone = phoneNumbers.FirstOrDefault(p => p.IdRelation == prospect.Id);
                prospect.Phone.PhoneNumber = Cipher.StringDecrypting(prospect.Phone.PhoneNumber);
            }
            return prospects;
        }

       public async Task<Prospect> GetSimpleCompleteProspect(string id)
        {
            var prospect = await _cnt.Prospects.FirstOrDefaultAsync(i => i.Id == id);
            if (prospect == null) return null;

            await LoadPhone(prospect);
            await LoadVehicles(prospect);
            await LoadDependents(prospect);
            await LoadSalesXp(prospect);
            await LoadJobInfo(prospect);
            await LoadSpouseInfo(prospect);

            return prospect;
        }

        private async Task LoadPhone(Prospect prospect)
        {
            prospect.Phone = await _cnt.PhoneNumbers.FirstOrDefaultAsync(p => p.IdRelation == prospect.IdDistributor);
            if (prospect.Phone?.PhoneNumber != null)
            {
                prospect.Phone.PhoneNumber = Cipher.StringDecrypting(prospect.Phone.PhoneNumber);
            }
        }

        private async Task LoadVehicles(Prospect prospect)
        {
            prospect.LVehicles = await _cnt.Vehicles.Where(v => v.IdRelation == prospect.IdDistributor).ToListAsync();
            foreach (var vehicle in prospect.LVehicles)
            {
                if (vehicle?.Price != null)
                {
                    vehicle.Price = Cipher.StringDecrypting(vehicle.Price);
                }
            }
        }

        private async Task LoadDependents(Prospect prospect)
        {
            prospect.LDependents = await _cnt.Dependents.Where(d => d.IdRelation == prospect.IdDistributor).ToListAsync();
            foreach (var dependent in prospect.LDependents)
            {
                if (dependent?.Income != null)
                {
                    dependent.Income = Cipher.StringDecrypting(dependent.Income);
                }
            }
        }

        private async Task LoadSalesXp(Prospect prospect)
        {
            prospect.LSalesXp = await _cnt.SalesXps.Where(s => s.IdDistributor == prospect.IdDistributor).ToListAsync();
            foreach (var salesXp in prospect.LSalesXp)
            {
                if (salesXp?.Limit != null)
                {
                    salesXp.Limit = Cipher.StringDecrypting(salesXp.Limit);
                }
                if (salesXp?.Comission != null)
                {
                    salesXp.Comission = Cipher.StringDecrypting(salesXp.Comission);
                }
            }
        }

        private async Task LoadJobInfo(Prospect prospect)
        {
            prospect.JobInfo = await _cnt.JobInfo.FirstOrDefaultAsync(j => j.IdRelation == prospect.IdDistributor);
            if (prospect.JobInfo?.Income != null)
            {
                prospect.JobInfo.Income = Cipher.StringDecrypting(prospect.JobInfo.Income);
            }
        }

        private async Task LoadSpouseInfo(Prospect prospect)
        {
            prospect.Spouse = await _cnt.Spouses.FirstOrDefaultAsync(s => s.IdRelation == prospect.IdDistributor);
            if (prospect.Spouse != null)
            {
                prospect.SpouseJob = await _cnt.JobInfo.FirstOrDefaultAsync(j => j.IdRelation == prospect.Spouse.Id);
                if (prospect.SpouseJob?.Income != null)
                {
                    prospect.SpouseJob.Income = Cipher.StringDecrypting(prospect.SpouseJob.Income);
                }
            }
        }
        
        public async Task<IEnumerable<_1.UsersManagement.Domain.Models.Tasks.Tasks>> GetAllAppointments()
        {
            return await _cnt.Tasks
                .Where(i => i.IdTaskType == Value.Appointment)
                .OrderBy(i => i.Date_Start)
                .ToListAsync();
        }

        public async Task<_1.UsersManagement.Domain.Models.Tasks.Tasks> UpdateTask(UpdateAppointmentDto request)
        {
            if(request.idStatus_mtz is not null)
            {
                request.idStatus_mtz = await _gUnit.Matriz.GetEntityProperty("Cancelada", "name", "id");
            }
            return await _gUnit.Task.UpdateEntity(request);
        }

    }
}