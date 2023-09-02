using System.Collections.Generic;

namespace _2.UsersManagement.Application.DTOs.Permissions.In_Services.GetPermissions.Views.NotGrouped
{
    public class ViewsControlsDto
    {
        public string Id { get; set; }
        public string NameView { get; set; }
        public string DescriptionView { get; set; }
        public string Path { get; set; }
        public bool Active { get; set; }
        public List<ControlDto> Controles { get; set; }
        
        public override bool Equals(object obj)
        {
            if (obj is ViewsControlsDto other)
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
