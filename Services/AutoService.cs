using AutoMapper;
using ConcesionarioApi.DTOs;
using ConcesionarioApi.Interfaces;
using ConcesionarioApi.Models;

namespace ConcesionarioApi.Services
{
    public class AutoService : IAutoService
    {
        private readonly IAutoRepository _repository;
        private readonly IMapper _mapper;

        public AutoService(IAutoRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AutoDto>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<AutoDto>>(entities);
        }

        public async Task<AutoDto?> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            return entity is null
                ? null
                : _mapper.Map<AutoDto>(entity);
        }

        public async Task<AutoDto> CreateAsync(CreateAutoDto createDto)
        {
            // mapear DTO -> entidad
            var entity = _mapper.Map<Auto>(createDto);
            await _repository.AddAsync(entity);

            // volver a mapear la entidad (ahora con Id asignado) a DTO
            return _mapper.Map<AutoDto>(entity);
        }

        public async Task UpdateAsync(int id, UpdateAutoDto updateDto)
        {
            var existing = await _repository.GetByIdAsync(id)
                ?? throw new KeyNotFoundException($"Auto con Id={id} no encontrado.");

            // aplicar cambios del DTO a la entidad
            _mapper.Map(updateDto, existing);
            await _repository.UpdateAsync(existing);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
