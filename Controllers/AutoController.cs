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
        private readonly IAutoService _svc;

        public AutoController(IAutoService svc)
        {
            _svc = svc;
        }

        [HttpGet]
        public async Task<IEnumerable<AutoDto>> Get() => (await _svc.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<ActionResult<AutoDto>> Get(int id) =>
            Ok(await _svc.GetByIdAsync(id));
        
        [HttpPost]
        public async Task<ActionResult<AutoDto>> Post(CreateAutoDto dto)
        {
            var created = await _svc.CreateAsync(dto);
            return CreatedAtAction(nameof(Get), new {id = created.Id}, created);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, UpdateAutoDto dto)
        {
            await _svc.UpdateAsync(id, dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _svc.DeleteAsync(id);
            return NoContent();
        }

    }
}
