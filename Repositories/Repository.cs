using ConcesionarioApi.Data;
using ConcesionarioApi.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ConcesionarioApi.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly ConcesionarioDbContext _concesionarioDbContext;
        protected readonly DbSet<TEntity> _dbSet;

        public Repository(ConcesionarioDbContext concesionarioDbContext)
        {
            _concesionarioDbContext = concesionarioDbContext;
            _dbSet = concesionarioDbContext.Set<TEntity>();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }

        public async Task<TEntity?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task AddAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
            await _concesionarioDbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(TEntity entity)
        {
            _dbSet.Update(entity);
            await _concesionarioDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                await _concesionarioDbContext.SaveChangesAsync();
            }
        }
    }
}
