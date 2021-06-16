using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(o => o.Address)
                .IsRequired();
            builder.Property(o => o.OrderDate)
                .IsRequired();

            builder.Property(x => x.OrderStatus).HasDefaultValue(OrderStatus.Hold);
            builder.Property(x => x.PaymentMethod).HasDefaultValue(PaymentMethod.Cash);

            builder.HasMany(o => o.OrderLines)
                .WithOne(ol => ol.Order)
                .HasForeignKey(ol => ol.OrderId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
