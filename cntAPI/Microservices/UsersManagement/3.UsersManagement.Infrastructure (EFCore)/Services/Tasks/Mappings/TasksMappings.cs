using System;
using _1.UsersManagement.Domain.Models.Tasks;
using _2.UsersManagement.Application.DTOs.Tasks;

namespace _3.UsersManagement.Infrastructure__EFCore_.Services.Tasks.Mappings
{
    public class TasksMappings
    {
        public static _1.UsersManagement.Domain.Models.Tasks.Tasks FillModelTask(InsertTaskDto request)
        {
            return new _1.UsersManagement.Domain.Models.Tasks.Tasks
            {
                Id = Guid.NewGuid().ToString().ToUpper(),
                IdUser = request.IdUser,
                IdTaskType = string.Empty,
                IdDestination = request.IdDestination,
                Date_Start = request.date_start,
                Date_End = request.date_end,
                Location = request.Location,
                Subject = request.Subject,
                Priority = request.Priority,
                Finished = false,
                Approved = false,
                IdStatus_mtz = request.idStatus_mtz,
                Active = true,
                CreatedAt = DateTime.Now,
                CreatedBy = request.CreatedBy,
                ModiffiedAt = DateTime.Now,
                ModiffiedBy = request.CreatedBy
            };
        }
    }
}