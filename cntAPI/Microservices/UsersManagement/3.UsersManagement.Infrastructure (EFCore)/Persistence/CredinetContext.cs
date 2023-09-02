using _1.UsersManagement.Domain.Models.Credinet;
using Microsoft.EntityFrameworkCore;

namespace _3.UsersManagement.Infrastructure__EFCore_.Persistence
{
    public class CredinetContext : DbContext
    {
        public CredinetContext(DbContextOptions<CredinetContext> options) : base(options) { }

        public CredinetContext() { }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
    }
}
