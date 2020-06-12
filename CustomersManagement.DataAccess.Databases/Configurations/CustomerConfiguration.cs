using CustomersManagement.DataAccess.Databases.Entities;
using CustomersManagement.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomersManagement.DataAccess.Databases.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        private readonly string schema;

        public CustomerConfiguration(string Schema) => schema = Schema;

        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder
               .ToTable("Customers", schema);

            builder.HasKey(x => x.Id).IsClustered(false);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.FirstName).IsRequired();
            builder.Property(x => x.LastName).IsRequired();
            builder.Property(x => x.CreatedOn).NewDateAutoInsertedOnCreate();
            builder.Property(x => x.UpdatedOn).NewDateAutoInsertedOnUpdate();
        }
    }
}
