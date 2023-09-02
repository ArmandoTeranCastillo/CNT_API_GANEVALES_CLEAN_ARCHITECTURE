namespace _1.UsersManagement.Domain.Models.Credinet
{
    public class AspNetUsers
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string CultureInfo { get; set; }
        public string TimeZoneId { get; set; }
        public bool Active { get; set; }
        public string Discriminator { get; set; }
        public int Profile_ProfileId { get; set; }
        public int UserIdentity { get; set; }
    }
}
