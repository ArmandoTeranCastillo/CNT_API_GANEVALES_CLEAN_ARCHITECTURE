using System.Collections.Generic;
using _2.UsersManagement.Application.DTOs.Permissions.In_Services.GetPermissions.Views.Grouped;

namespace _2.UsersManagement.Application.DTOs.Permissions.In_Services.GetPermissions.Views
{
    public class ViewDto
    {
        public string Id { get; set; }
        public string NameView { get; set; }
        public string DescriptionView { get; set; }
        public string Path { get; set; }
        public bool Active { get; set; }
        public List<ControlsGroupDto> GrupoControles { get; set; }
        public List<ControlDto> Controles { get; set; }
        
        public override bool Equals(object obj)
        {
            if (obj is ViewDto other)
            {
                return Id == other.Id; 
            }
            return false;
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode(); 
        }
    }
}
