using Microsoft.AspNetCore.Mvc;
using ConcesionarioApi.Models;
using ConcesionarioApi.Services;
using ConcesionarioApi.DTOs;

namespace ConcesionarioApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AutoController : ControllerBase
    {
        private readonly IAutoService _autoService;

        public AutoController(IAutoService autoService)
        {
            _autoService = autoService;
        }

        [HttpGet]
        public async Task<IEnumerable<AutoDto>> Get() => (await _autoService.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<ActionResult<AutoDto>> Get(int id) =>
            Ok(await _autoService.GetByIdAsync(id));
        
        [HttpPost]
        public async Task<ActionResult<AutoDto>> Post(CreateAutoDto dto)
        {
            var created = await _autoService.CreateAsync(dto);
            return CreatedAtAction(nameof(Get), new {id = created.Id}, created);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, UpdateAutoDto dto)
        {
            await _autoService.UpdateAsync(id, dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _autoService.DeleteAsync(id);
            return NoContent();
        }

    }
}
