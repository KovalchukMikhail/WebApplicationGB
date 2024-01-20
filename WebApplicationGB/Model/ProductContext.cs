using Microsoft.EntityFrameworkCore;

namespace WebApplicationGB.Model
{
    public class ProductContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Storage> Storages { get; set; }

        public ProductContext() { }
        public ProductContext(DbContextOptions<ProductContext> options) : base(options) { }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(@"Server=.;DataBase=WebStore;Integrated Security=False;TrustServerCertificate=true;Trusted_Connection=True;")
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
