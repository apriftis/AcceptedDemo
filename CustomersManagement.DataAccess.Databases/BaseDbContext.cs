using CustomersManagement.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace CustomersManagement.DataAccess.Databases
{
    public abstract class BaseDbContext : DbContext
    {
        protected BaseDbContext() : base()
        {
        }

        protected BaseDbContext(DbContextOptions options) : base(options) { }

        public override sealed int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            this.ApplyModificationTimestampChanges();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override sealed int SaveChanges()
        {
            return SaveChanges(true);
        }

        public override sealed Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            this.ApplyModificationTimestampChanges();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public override sealed Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return SaveChangesAsync(true, cancellationToken);
        }
    }
}
