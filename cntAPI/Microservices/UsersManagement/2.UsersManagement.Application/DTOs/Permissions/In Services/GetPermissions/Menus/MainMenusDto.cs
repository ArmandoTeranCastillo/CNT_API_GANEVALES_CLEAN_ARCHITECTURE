namespace _2.UsersManagement.Application.DTOs.Permissions.In_Services.GetPermissions.Menus
{
    public class MainMenusDto
    {
        public string NameMenu { get; set; }
        public string DescriptionMenu { get; set; }
        public string IconLibrary { get; set; }
        public string IconPaths { get; set; }
        public string Route { get; set; }
        public int OrderMenu { get; set; }
        public bool Active { get; set; }
        
        public override bool Equals(object obj)
        {
            if (obj is MainMenusDto other)
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
