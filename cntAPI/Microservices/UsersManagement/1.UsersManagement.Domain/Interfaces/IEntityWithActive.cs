namespace _1.UsersManagement.Domain.Interfaces
{
    public interface IEntityWithActive
    {
        string Id { get; set; }
        bool Active { get; set; }
    }
}
