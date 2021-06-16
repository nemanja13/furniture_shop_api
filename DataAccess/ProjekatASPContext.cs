using DataAccess.Configurations;
using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess
{
    public class ProjekatASPContext : DbContext
    {

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new ColorConfiguration());
            modelBuilder.ApplyConfiguration(new CommentConfiguration());
            modelBuilder.ApplyConfiguration(new MaterialConfiguration());
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new OrderLineConfiguration());
            modelBuilder.ApplyConfiguration(new PriceConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new RatingConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new UseCaseLogConfiguration());

            modelBuilder.Entity<ColorProduct>().HasKey(x => new { x.ColorId, x.ProductId });
            modelBuilder.Entity<MaterialProduct>().HasKey(x => new { x.MaterialId, x.ProductId });

            modelBuilder.Entity<Category>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<Product>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<User>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<Material>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<Color>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<Price>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<Rating>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<Comment>().HasQueryFilter(x => !x.IsDeleted);
        }

        public override int SaveChanges()
        {
            foreach(var entry in ChangeTracker.Entries())
            {
                if(entry.Entity is Entity e)
                {
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            e.CreatedAt = DateTime.UtcNow;
                            e.IsActive = true;
                            e.ModifiedAt = null;
                            e.DeletedAt = null;
                            e.IsDeleted = false;
                            break;
                        case EntityState.Modified:
                            e.ModifiedAt = DateTime.UtcNow;
                            break;
                    }
                }
            }
            return base.SaveChanges();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=.\\SQLEXPRESS;Initial Catalog=ApiAspProjekat;Integrated Security=True");
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderLine> OrderLines { get; set; }
        public DbSet<Price> Prices { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ColorProduct> ColorProducts { get; set; }
        public DbSet<MaterialProduct> MaterialProducts { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserUseCase> UserUseCases { get; set; }
        public DbSet<UseCaseLog> UseCaseLogs { get; set; }
    }
}
