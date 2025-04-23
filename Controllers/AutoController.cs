using Microsoft.AspNetCore.Mvc;
using ConcesionarioApi.Models;
using ConcesionarioApi.Services;

namespace ConcesionarioApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AutoController : ControllerBase
    {
        private readonly AutoService _service;

        public AutoController(AutoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get() => Ok(await _service.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var auto = await _service.GetByIdAsync(id);
            return auto == null ? NotFound() : Ok(auto);
        }
        [HttpPost]
        public async Task<IActionResult> Create(Auto auto)
        {
            await _service.AddAsync(auto);
            return CreatedAtAction(nameof(GetById), new { id = auto.Id }, auto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Auto auto)
        {
            if (id != auto.Id) return BadRequest();
            await _service.UpdateAsync(auto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }

    }
}
