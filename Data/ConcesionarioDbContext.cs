using Microsoft.EntityFrameworkCore;
using ConcesionarioApi.Models;

namespace ConcesionarioApi.Data
{
    public class ConcesionarioDbContext : DbContext
    {
        public  ConcesionarioDbContext(DbContextOptions<ConcesionarioDbContext> options) : base(options) { }

        public DbSet<Auto> Autos => Set<Auto>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Auto>(entity =>
            {
                entity.ToTable("Autos");
                entity.HasKey(a => a.Id);
                entity.Property(a => a.Marca)
                .IsRequired()
                .HasMaxLength(100);
                entity.Property(a => a.Modelo)
                .IsRequired()
                .HasMaxLength(100);
                entity.Property(a => a.Precio)
                .HasColumnType("decimal(18, 2)")
                .IsRequired();
                entity.Property(a => a.Anio)
                .IsRequired();
                entity.HasIndex(a => new { a.Marca, a.Modelo })
                .HasDatabaseName("IX_Autos_Marca_Modelo");
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
