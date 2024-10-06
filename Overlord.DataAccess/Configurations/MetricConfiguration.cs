using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Overlord.Domain.Entities;

namespace Overlord.DataAccess.Configurations
{
    public class MetricConfiguration : BaseConfiguration<Metric>
    {
        protected override void ConfigureConcrete(EntityTypeBuilder<Metric> builder)
        {
            builder.Property(t => t.MetricId).IsRequired();

            builder.HasIndex(t => t.Name).IsUnique();

            builder.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(t => t.Description)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}
