using ConcesionarioApi.Models;
using ConcesionarioApi.Interfaces;

namespace ConcesionarioApi.Services
{
    public class AutoService
    {
        private readonly IAutoRepository _repository;

        public AutoService(IAutoRepository repository)
        {
            _repository = repository;
        }
        public Task<IEnumerable<Auto>> GetAllAsync() => _repository.GetAllAsync();
        public Task<Auto?> GetByIdAsync(int id) => _repository.GetByIdAsync(id);
        public Task AddAsync(Auto auto) => _repository.AddAsync(auto);
        public Task UpdateAsync(Auto auto) => _repository.UpdateAsync(auto);
        public Task DeleteAsync(int id) => _repository.DeleteAsync(id);
    }
}
