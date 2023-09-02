namespace _1.UsersManagement.Domain.Interfaces
{
    public interface IDistributorsEntity
    {
        public string Id { get; set; }
        public string IdDistributor { get; set; }
        public bool SalesXp { get; set; }
        public int Dependents { get; set; }
    }
}