using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasIndex(u => u.Email)
                .IsUnique();
            builder.Property(u => u.Email)
                .IsRequired();
            builder.Property(u => u.FirstName)
                .IsRequired()
                .HasMaxLength(30);
            builder.Property(u => u.LastName)
                .IsRequired()
                .HasMaxLength(30);
            builder.Property(u => u.Password)
                .IsRequired();
            builder.Property(u => u.Phone)
                .IsRequired();


            builder.HasMany(u => u.Orders)
                .WithOne(o => o.User)
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(u => u.Comments)
                .WithOne(c => c.User)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(u => u.Ratings)
                .WithOne(r => r.User)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(u => u.UserUseCases)
                .WithOne(uuc => uuc.User)
                .HasForeignKey(uuc => uuc.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
