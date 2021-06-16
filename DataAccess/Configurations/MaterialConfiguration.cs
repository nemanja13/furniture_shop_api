using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Configurations
{
    public class MaterialConfiguration : IEntityTypeConfiguration<Material>
    {
        public void Configure(EntityTypeBuilder<Material> builder)
        {
            builder.Property(m => m.Name)
               .IsRequired()
               .HasMaxLength(30);
            builder.HasIndex(m => m.Name)
                .IsUnique();

            builder.HasMany(m => m.MaterialProducts)
                .WithOne(mp => mp.Material)
                .HasForeignKey(mp => mp.MaterialId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
