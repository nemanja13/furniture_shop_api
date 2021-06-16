using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Configurations
{
    public class OrderLineConfiguration : IEntityTypeConfiguration<OrderLine>
    {
        public void Configure(EntityTypeBuilder<OrderLine> builder)
        {
            builder.Property(ol => ol.Name)
                .IsRequired();
            builder.Property(ol => ol.Quantity)
                .HasDefaultValue(1);
            builder.Property(ol => ol.Price)
                .IsRequired();
        }
    }
}
