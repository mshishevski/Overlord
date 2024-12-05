using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Overlord.Domain.Entities;

namespace Overlord.DataAccess.Configurations
{
    public class ClientConfiguration : BaseConfiguration<Client>
    {
        protected override void ConfigureConcrete(EntityTypeBuilder<Client> builder)
        {
            builder.Property(t => t.ClientId).IsRequired();

            builder.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}
