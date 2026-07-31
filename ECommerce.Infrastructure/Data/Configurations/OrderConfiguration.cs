using ECommerce.Domain.Entity.orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Infrastructure.Data.Configurations
{
    internal class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");

            builder.Property(o => o.SubTotal).HasColumnType("decimal(10,2)");
            builder.Property(o => o.BuyerEmail).IsRequired().HasMaxLength(256);
            builder.Property(o => o.Status).HasConversion<string>().HasMaxLength(50);

            builder.HasMany(o => o.Items)
                   .WithOne()
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(o => o.DeliveryMethod)
                   .WithMany()
                   .HasForeignKey(o => o.DeliveryMethodId);

            builder.OwnsOne(o => o.ShipToAddress);
        }
    }
}
