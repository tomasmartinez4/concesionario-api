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
            base.OnModelCreating(modelBuilder);

            var auto = modelBuilder.Entity<Auto>();
            auto.ToTable("Autos");
            auto.HasKey(a => a.Id);
            auto.Property(a => a.Marca)
                .IsRequired()
                .HasMaxLength(100);
            auto.Property(a => a.Modelo)
                .IsRequired()
                .HasMaxLength(100);
            auto.Property(a => a.Anio)
                .IsRequired();
            auto.Property(a => a.Precio)
                .IsRequired()
                .HasPrecision(10, 2);
        }
    }
}
