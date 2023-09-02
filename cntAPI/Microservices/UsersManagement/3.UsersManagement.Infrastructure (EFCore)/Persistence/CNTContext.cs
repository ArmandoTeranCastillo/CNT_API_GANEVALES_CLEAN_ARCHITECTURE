using _1.UsersManagement.Domain.Models.Addresses;
using _1.UsersManagement.Domain.Models.ControlValues;
using _1.UsersManagement.Domain.Models.Credinet;
using _1.UsersManagement.Domain.Models.Distributors;
using _1.UsersManagement.Domain.Models.Documents;
using _1.UsersManagement.Domain.Models.Exceptions;
using _1.UsersManagement.Domain.Models.Files;
using _1.UsersManagement.Domain.Models.Language;
using _1.UsersManagement.Domain.Models.Matrices;
using _1.UsersManagement.Domain.Models.Permissions;
using _1.UsersManagement.Domain.Models.Phones;
using _1.UsersManagement.Domain.Models.Tasks;
using _1.UsersManagement.Domain.Models.Users;
using Microsoft.EntityFrameworkCore;

namespace _3.UsersManagement.Infrastructure__EFCore_.Persistence
{
    public class CNTContext : DbContext
    {
        public CNTContext(DbContextOptions<CNTContext> options) : base(options){ }

        public CNTContext() { }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<UserRoles> UserRoles { get; set; }
        public virtual DbSet<UserTypes> UserTypes { get; set; }
        public virtual DbSet<Profiles> Profiles { get; set; }
        public virtual DbSet<PhoneNumbers> PhoneNumbers { get; set; }
        public virtual DbSet<PhoneTypes> PhoneTypes { get; set; }
        public virtual DbSet<PhoneLadas> PhoneLadas { get; set; }
        public virtual DbSet<Tasks> Tasks { get; set; }
        public virtual DbSet<TasksTypes> TaskTypes { get; set; }
        public virtual DbSet<Menus> Menus { get; set; }
        public virtual DbSet<Views> Views { get; set; }
        public virtual DbSet<Controls> Controls { get; set; }
        public virtual DbSet<Relations> Relations { get; set; }
        public virtual DbSet<Matrices> Matrices { get; set; }
        public virtual DbSet<MatrizType> MatrizType { get; set; }
        public virtual DbSet<Languages> Languages { get; set; }
        public virtual DbSet<FilesPaths> FilesPaths { get; set; }
        public virtual DbSet<LogErrors> LogErrors { get; set; }
        public virtual DbSet<LogsLogin> LogsLogin { get; set; }
        public virtual DbSet<ErrorCodes> ErrorCodes { get; set; }
        public virtual DbSet<CredinetRelations> CredinetRelations { get; set; }
        public virtual DbSet<DoctoUsers> DoctoUsers { get; set; }
        public virtual DbSet<DoctoReqs> DoctoReqs { get; set; }
        public virtual DbSet<DoctoTypes> DoctoTypes { get; set; }
        public virtual DbSet<Distributors> Distributors { get; set; }
        public virtual DbSet<Prospect> Prospects { get; set; }
        public virtual DbSet<Avals> Avals { get; set; }
        public virtual DbSet<References> References { get; set; }
        public virtual DbSet<Dependents> Dependents { get; set; }
        public virtual DbSet<SalesXp> SalesXps { get; set; }
        public virtual DbSet<JobInfo> JobInfo { get; set; }
        public virtual DbSet<Spouse> Spouses { get; set; }
        public virtual DbSet<Vehicles> Vehicles { get; set; }
        public virtual DbSet<Countries> Countries { get; set; }
        public virtual DbSet<States> States { get; set; }
        public virtual DbSet<Municipalities> Municipalities { get; set; }
        public virtual DbSet<Cities> Cities { get; set; }
        public virtual DbSet<Zipcodes> Zipcodes { get; set; }
        public virtual DbSet<Addresses> Addresses { get; set; }
        public virtual DbSet<ControlValues> ControlValues { get; set; }
    }
}
