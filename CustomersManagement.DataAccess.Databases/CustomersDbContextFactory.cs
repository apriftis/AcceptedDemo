using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CustomersManagement.DataAccess.Databases
{
    public class CustomersDbContextFactory : IDesignTimeDbContextFactory<CustomerDbContext>
    {
        public CustomerDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<CustomerDbContext>().UseSqlServer("Data Source=localhost; Initial catalog=CustomersManagement;Integrated security=True", sqlOptions =>
            {
                sqlOptions.MigrationsHistoryTable(CustomerDbContext.MigrationTableName, CustomerDbContext.Schema);
            });

            return new CustomerDbContext(optionsBuilder.Options);
        }
    }
}