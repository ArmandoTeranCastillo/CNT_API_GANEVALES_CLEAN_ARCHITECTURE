using System.Collections.Generic;

namespace _2.UsersManagement.Application.DTOs.Permissions.In_Services.GetPermissions.Views.Grouped
{
    public class ControlsGroupDto
    {
        public string Id { get; set; }
        public string NameControl { get; set; }
        public string DescriptionControl { get; set; }
        public bool Active { get; set; }
        public List<ControlDto> Controles { get; set; }
        
        public override bool Equals(object obj)
        {
            if (obj is ControlsGroupDto other)
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
