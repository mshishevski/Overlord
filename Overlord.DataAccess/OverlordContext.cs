using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Overlord.Application;
using Overlord.Domain.Base;
using Overlord.Domain.Entities;

namespace Overlord.DataAccess
{
    public class OverlordContext : DbContext, IOverlordContext
    {
        public OverlordContext(DbContextOptions<OverlordContext> options): base(options) { }

        public DbSet<Metric> Metrics => Set<Metric>();

        public new async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var changedEntries = ChangeTracker.Entries()
                .Where(e => e.State is EntityState.Modified or EntityState.Added);

            SetBaseModelProperties(changedEntries);

            await base.SaveChangesAsync(cancellationToken);
        }

        private static void SetBaseModelProperties(IEnumerable<EntityEntry> changedEntries)
        {
            foreach (var entry in changedEntries)
            {
                if (entry.Entity is BaseModel baseModel)
                {
                    baseModel.ChangedBy = "Admin";
                    baseModel.ChangedTime = DateTime.Now;

                    if (entry.State is EntityState.Added)
                    {
                        baseModel.CreatedBy = "Admin";
                        baseModel.CreationTime = DateTime.Now;
                        
                    }
                }
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(DataAccessAssembly.Get());
        }
    }
}
