using ECommerce.Domain.Entity.orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Infrastructure.Data.Configurations
{
    internal class DeliveryMethodConfiguration : IEntityTypeConfiguration<DeliveryMethod>
    {
        public void Configure(EntityTypeBuilder<DeliveryMethod> builder)
        {
            builder.ToTable("DeliveryMethods");

            builder.Property(d => d.Cost).HasColumnType("decimal(8,2)");
            builder.Property(d => d.ShortName).HasColumnType("varchar").HasMaxLength(50);
            builder.Property(d => d.Description).HasColumnType("varchar").HasMaxLength(100);
            builder.Property(d => d.DeliveryTime).HasColumnType("varchar").HasMaxLength(50);
        }
    }
}
