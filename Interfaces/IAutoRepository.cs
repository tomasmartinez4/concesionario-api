using ConcesionarioApi.Models;

namespace ConcesionarioApi.Interfaces
{
    public interface IAutoRepository
    {
        Task<IEnumerable<Auto>> GetAllAsync();
        Task<Auto?> GetByIdAsync(int id);
        Task AddAsync (Auto auto);
        Task UpdateAsync (Auto auto);
        Task DeleteAsync (int id);
    }
}
