using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Configurations
{
    public class ColorConfiguration : IEntityTypeConfiguration<Color>
    {
        public void Configure(EntityTypeBuilder<Color> builder)
        {
            builder.Property(c => c.Name)
               .IsRequired()
               .HasMaxLength(30);
            builder.HasIndex(c => c.Name)
                .IsUnique();

            builder.HasMany(c => c.ColorProducts)
                .WithOne(cp => cp.Color)
                .HasForeignKey(cp => cp.ColorId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
