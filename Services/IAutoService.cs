using ConcesionarioApi.DTOs;

namespace ConcesionarioApi.Services
{
    public interface IAutoService
    {
        Task<IEnumerable<AutoDto>> GetAllAsync();
        Task<AutoDto> GetByIdAsync(int id);
        Task<AutoDto> CreateAsync(CreateAutoDto dto);
        Task UpdateAsync(int id, UpdateAutoDto dto);
        Task DeleteAsync(int id);
    }
}
