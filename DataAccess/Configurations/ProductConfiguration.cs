using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(p => p.Name)
                .IsRequired();
            builder.HasIndex(p => p.Name)
                .IsUnique();
            builder.Property(p => p.FullName)
                .IsRequired();
            builder.Property(p => p.Description)
                .IsRequired();
            builder.Property(p => p.Dimensions)
                .IsRequired();
            builder.Property(p => p.Quantity)
                .IsRequired();
            builder.Property(p => p.Image)
                .IsRequired();

            builder.HasMany(p => p.OrderLines)
                .WithOne(ol => ol.Product)
                .HasForeignKey(ol => ol.ProductId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(p => p.Comments)
                .WithOne(c => c.Product)
                .HasForeignKey(c => c.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p => p.ProductColors)
                .WithOne(pc => pc.Product)
                .HasForeignKey(pc => pc.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p => p.ProductMaterials)
                .WithOne(pm => pm.Product)
                .HasForeignKey(pm => pm.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p => p.Prices)
                .WithOne(pr => pr.Product)
                .HasForeignKey(pr => pr.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(p => p.Ratings)
                .WithOne(r => r.Product)
                .HasForeignKey(r => r.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
