using Data.Mapping;
using Domain.Entities;
using System.Data.Entity;

namespace Data
{
    public class Context : DbContext
    {
        public Context()
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
            Configuration.ValidateOnSaveEnabled = false;            
        }

        public DbSet<Group> Groups { get; set; } 

        public DbSet<Site> Sites { get; set; }

        public DbSet<Transaction> Transactions { get; set; }

        public DbSet<PatternTime> PatternTimes { get; set; }

        public DbSet<Downtime> Downtimes { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<Module> Modules { get; set; } 

        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new EmployeeMap());
        }
    }
}