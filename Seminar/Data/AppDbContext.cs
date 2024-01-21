using Microsoft.EntityFrameworkCore;
using Seminar.Models;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Seminar.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Storage> Storages { get; set; }

        public AppDbContext() { }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(@"Server=.;DataBase=GraphQlSeminar;Integrated Security=False;TrustServerCertificate=true;Trusted_Connection=True;")
        //        .UseLazyLoadingProxies();
        //}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Products");

                entity.HasKey(e => e.Id).HasName("ProductID");
                entity.HasIndex(e => e.Name).IsUnique();

                entity.Property(e => e.Description)
                    .HasColumnName("Description")
                    .HasMaxLength(255);

                entity.Property(e => e.Cost)
                    .HasColumnName("Cost")
                    .IsRequired();
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Categories");

                entity.HasKey(e => e.Id).HasName("CategoryID");
                entity.HasIndex(e => e.Name).IsUnique();

                entity.Property(e => e.Name)
                .HasColumnName("CategoryName")
                .HasMaxLength(255);
            });

            modelBuilder.Entity<Storage>(entity =>
            {
                entity.ToTable("Storage");

                entity.HasKey(e => e.Id).HasName("StorageID");

                entity.Property(e => e.Name)
                .HasColumnName("StorageName");

                entity.Property(e => e.Count)
                .HasColumnName("ProductCount");

                entity.HasMany(e => e.Products)
                .WithMany(c => c.Storages)
                .UsingEntity(j => j.ToTable("ProductStorage"));
            });
        }
    }
}
