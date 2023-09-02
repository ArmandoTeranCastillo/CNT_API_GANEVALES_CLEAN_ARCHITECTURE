namespace _2.UsersManagement.Application.DTOs.Permissions.In_Services.GetPermissions.Views
{
    public class ControlDto
    {
        public string Id { get; set; }
        public string NameControl { get; set; }
        public string DescriptionControl { get; set; }
        public string Route { get; set; }
        public string RouteIcon { get; set; }
        public bool Active { get; set; }
        
        public override bool Equals(object obj)
        {
            if (obj is ControlDto other)
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
