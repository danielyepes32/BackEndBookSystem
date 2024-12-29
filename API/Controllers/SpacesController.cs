using Backend_agendamientos.Core.Entities;
using Backend_agendamientos.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Backend_agendamientos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SpacesController : ControllerBase
    {
        private readonly ISpaceRepository _repository;

        public SpacesController(ISpaceRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetSpaces()
        {
            var spaces = await _repository.GetSpacesAsync();
            return Ok(spaces);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSpace(Space space)
        {
            await _repository.AddSpaceAsync(space);
            return CreatedAtAction(nameof(GetSpaces), new { id = space.Id }, space);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSpace(int id)
        {
            await _repository.DeleteSpaceAsync(id);
            return NoContent();
        }
    }
}
