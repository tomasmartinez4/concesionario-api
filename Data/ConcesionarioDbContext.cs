using Microsoft.EntityFrameworkCore;
using ConcesionarioApi.Models;

namespace ConcesionarioApi.Data
{
    public class ConcesionarioDbContext : DbContext
    {
        public  ConcesionarioDbContext(DbContextOptions<ConcesionarioDbContext> options) : base(options) { }

        public DbSet<Auto> Autos => Set<Auto>();
    }
}
