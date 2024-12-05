using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Overlord.Domain.Entities;

namespace Overlord.DataAccess.Configurations
{
    public class FloorConfiguration : BaseConfiguration<Floor>
    {
        protected override void ConfigureConcrete(EntityTypeBuilder<Floor> builder)
        {
            builder.Property(t => t.FloorId).IsRequired();

            builder.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}
