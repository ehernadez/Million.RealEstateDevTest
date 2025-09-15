using Microsoft.EntityFrameworkCore;
using Million.RealEstate.Domain.Entities;

namespace Million.RealEstate.Infrastructure.EntityFramework.Data
{
    public class RealEstateDbContext : DbContext
    {
        public RealEstateDbContext(DbContextOptions<RealEstateDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Property> Properties { get; set; } = null!;
        public DbSet<PropertyTrace> PropertyTraces { get; set; } = null!;
        public DbSet<PropertyImage> PropertyImages { get; set; } = null!;
        public DbSet<Owner> Owners { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuración de Owner
            modelBuilder.Entity<Owner>()
                .HasKey(o => o.IdOwner);

            // Configuración de Property
            modelBuilder.Entity<Property>()
                .HasKey(p => p.IdProperty);

            modelBuilder.Entity<Property>()
                .HasOne(p => p.Owner)
                .WithMany(o => o.Properties)
                .HasForeignKey(p => p.IdOwner)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);

            // Configuración de PropertyImage
            modelBuilder.Entity<PropertyImage>()
                .HasKey(pi => pi.IdPropertyImage);

            modelBuilder.Entity<PropertyImage>()
                .HasOne(pi => pi.Property)
                .WithMany(p => p.Images)
                .HasForeignKey(pi => pi.IdProperty)
                .OnDelete(DeleteBehavior.Cascade);

            // Configuración de PropertyTrace
            modelBuilder.Entity<PropertyTrace>()
                .HasKey(pt => pt.IdPropertyTrace);

            modelBuilder.Entity<PropertyTrace>()
                .HasOne(pt => pt.Property)
                .WithMany(p => p.Traces)
                .HasForeignKey(pt => pt.IdProperty)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.Email).IsUnique();
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
