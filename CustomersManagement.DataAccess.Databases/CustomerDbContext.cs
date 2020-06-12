using CustomersManagement.DataAccess.Databases.Configurations;
using CustomersManagement.DataAccess.Databases.Entities;
using CustomersManagement.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CustomersManagement.DataAccess.Databases
{
    public class CustomerDbContext : BaseDbContext
    {
        public const string MigrationTableName = "_CustomerMigratioHistory";
        public const string Schema = "dbo";

        public CustomerDbContext(DbContextOptions<CustomerDbContext> options) : base(options)
        {
        }


        public DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new CustomerConfiguration(Schema));
        }
    }
}
