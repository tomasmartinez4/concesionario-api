using ConcesionarioApi.Data;
using ConcesionarioApi.Interfaces;
using ConcesionarioApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ConcesionarioApi.Repositories
{
    public class AutoRepository : Repository<Auto>, IAutoRepository
    {
        public AutoRepository(ConcesionarioDbContext context) : base(context)
        {
        }
    }
}
