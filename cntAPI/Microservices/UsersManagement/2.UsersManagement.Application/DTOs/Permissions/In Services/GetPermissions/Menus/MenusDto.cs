using System.Collections.Generic;

namespace _2.UsersManagement.Application.DTOs.Permissions.In_Services.GetPermissions.Menus
{
    public class MenusDto
    {
        public string NameMenu { get; set; }
        public string DescriptionMenu { get; set; }
        public string IconLibrary { get; set; }
        public string IconPaths { get; set; }
        public string Route { get; set; }
        public int OrderMenu { get; set; }
        public bool Active { get; set; }
        public List<SubMenuDto> SubMenus { get; set; }
        
        public override bool Equals(object obj)
        {
            if (obj is MenusDto other)
            {
                return NameMenu == other.NameMenu; 
            }
            return false;
        }
        public override int GetHashCode()
        {
            return NameMenu.GetHashCode(); 
        }
    }
}
