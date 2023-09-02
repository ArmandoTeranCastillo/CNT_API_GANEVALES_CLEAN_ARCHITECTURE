using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2.UsersManagement.Application.DTOs.Permissions.In_Services.GetPermissions;

namespace _2.UsersManagement.Application.Interfaces.Permissions
{
    public interface IPermissionsService
    {
        Task<PermissionsDto> GetUserPermissions(int operation, string id, string language);
    }
}
