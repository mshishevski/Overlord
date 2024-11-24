using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Overlord.Domain.Base;

namespace Overlord.DataAccess.Configurations
{
    public abstract class BaseConfiguration<T> : IEntityTypeConfiguration<T> where T : BaseModel
    {
        public void Configure(EntityTypeBuilder<T> builder)
        {
            builder.Property(t => t.CreatedBy)
                .IsRequired()
                .HasMaxLength(64);

            builder.Property(m => m.ChangedBy)
                .IsRequired()
                .HasMaxLength(64);

            ConfigureConcrete(builder);
        }

        protected abstract void ConfigureConcrete(EntityTypeBuilder<T> builder);
    }
}

