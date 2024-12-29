using Backend_agendamientos.Core.Entities;
using Backend_agendamientos.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Backend_agendamientos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservationsController : ControllerBase
    {
        private readonly IReservationRepository _repository;

        public ReservationsController(IReservationRepository repository)
        {
            _repository = repository;
        }

        [HttpGet] // Endpoint para obtener todas las reservas
        public async Task<IActionResult> GetReservations()
        {
            var reservations = await _repository.GetReservationsAsync();
            return Ok(reservations);
        }

        [HttpGet("{id}")] // Endpoint para obtener una reserva específica por ID
        public async Task<IActionResult> GetReservationById(int id)
        {
            var reservation = await _repository.GetReservationByIdAsync(id);
            if (reservation == null)
                return NotFound();

            return Ok(reservation);
        }

        [HttpGet("space/{id}")] // Endpoint para obtener reservas por SpaceId con rango de fechas opcional
        public async Task<IActionResult> GetReservationsBySpace(
            int id,
            [FromQuery] DateTime? startDate = null,
            [FromQuery] DateTime? endDate = null)
        {
            var reservations = await _repository.GetReservationBySpaceId(id, startDate, endDate);
            if (reservations == null || !reservations.Any())
                return NotFound("No reservations found for the specified space and date range.");

            return Ok(reservations);
        }

        [HttpGet("user/{id}")] // Endpoint para obtener reservas por UserId con rango de fechas opcional
        public async Task<IActionResult> GetReservationsByUser(
            int id,
            [FromQuery] DateTime? startDate = null,
            [FromQuery] DateTime? endDate = null)
        {
            var reservations = await _repository.GetReservationByUserId(id, startDate, endDate);
            if (reservations == null || !reservations.Any())
                return NotFound("No reservations found for the specified user and date range.");

            return Ok(reservations);
        }

        [HttpPost] // Endpoint para crear una reserva
        public async Task<IActionResult> CreateReservation(Reservation reservation)
        {
            if (await _repository.IsOverlappingAsync(reservation.SpaceId, reservation.StartDate, reservation.EndDate))
                return BadRequest("Reservation overlaps with an existing one.");

            await _repository.AddReservationAsync(reservation);
            return CreatedAtAction(nameof(GetReservationById), new { id = reservation.Id }, reservation);
        }

        [HttpDelete("{id}")] // Endpoint para eliminar una reserva
        public async Task<IActionResult> DeleteReservation(int id)
        {
            await _repository.DeleteReservationAsync(id);
            return NoContent();
        }
    }
}
