using System.Collections.Generic;
using System.Text.Json.Serialization;
using _2.UsersManagement.Application.DTOs.Permissions.In_Services.GetPermissions.Menus;
using _2.UsersManagement.Application.DTOs.Permissions.In_Services.GetPermissions.Views;

namespace _2.UsersManagement.Application.DTOs.Permissions.In_Services.GetPermissions
{
    public class PermissionsDto
    {
        [JsonIgnore]
        public List<MenusDto> Menus { get; set; }
        public List<MenusDto> MainMenus { get; set; }
        public List<ViewDto> Vistas { get; set; }
    }
}
