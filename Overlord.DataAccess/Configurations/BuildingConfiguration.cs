using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Overlord.Domain.Entities;


namespace Overlord.DataAccess.Configurations
{
    public class BuildingConfiguration : BaseConfiguration<Building>
    {
        protected override void ConfigureConcrete(EntityTypeBuilder<Building> builder)
        {
            builder.Property(t => t.BuildingId).IsRequired();

            builder.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}
