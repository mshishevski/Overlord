using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Overlord.Domain.Entities;

namespace Overlord.DataAccess.Configurations
{

    public class RoomConfiguration : BaseConfiguration<Room>
    {
        protected override void ConfigureConcrete(EntityTypeBuilder<Room> builder)
        {
            builder.Property(t => t.FloorId).IsRequired();

            builder.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}
