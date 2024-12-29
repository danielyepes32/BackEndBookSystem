using Backend_agendamientos.Core.Entities;
using Backend_agendamientos.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Backend_agendamientos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _repository;

        public UsersController(IUserRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _repository.GetUsersAsync();
            return Ok(users);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(User user)
        {
            await _repository.AddUserAsync(user);
            return CreatedAtAction(nameof(GetUsers), new { id = user.Id }, user);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            await _repository.DeleteUserAsync(id);
            return NoContent();
        }
    }
}
