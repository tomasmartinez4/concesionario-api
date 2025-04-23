using ConcesionarioApi.Data;
using ConcesionarioApi.Interfaces;
using ConcesionarioApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ConcesionarioApi.Repositories
{
    public class AutoRepository : IAutoRepository
    {
        private readonly ConcesionarioDbContext _context;
        public AutoRepository(ConcesionarioDbContext context) 
        {
            _context = context;
        }

        public async Task<IEnumerable<Auto>> GetAllAsync() =>
            await _context.Autos.ToListAsync();

        public async Task<Auto?> GetByIdAsync(int id) =>
            await _context.Autos.FindAsync(id);

        public async Task AddAsync(Auto auto)
        {
            _context.Autos.Add(auto);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Auto auto)
        {
            _context.Update(auto);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var auto = await GetByIdAsync(id);
            if (auto != null)
            {
                _context.Autos.Remove(auto);
                await _context.SaveChangesAsync();
            }
        }
    }
}
