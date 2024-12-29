using Backend_agendamientos.Core.Entities;
using Backend_agendamientos.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Backend_agendamientos.Infrastructure.Persistence
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly AppDbContext _context;
        private const int MaxReservationDurationMinutes = 240; // Duración máxima en minutos (4 horas)

        public ReservationRepository(AppDbContext context)
        {
            _context = context;
        }

        // Agregar una nueva reserva con validación de duración máxima
        public async Task AddReservationAsync(Reservation reservation)
        {
            var duration = (reservation.EndDate - reservation.StartDate).TotalMinutes;
            if (duration > MaxReservationDurationMinutes)
            {
                throw new InvalidOperationException($"La reserva no puede exceder los {MaxReservationDurationMinutes} minutos.");
            }

            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();
        }

        // Eliminar una reserva existente por ID
        public async Task DeleteReservationAsync(int id)
        {
            var reservation = await GetReservationByIdAsync(id);
            if (reservation != null)
            {
                _context.Reservations.Remove(reservation);
                await _context.SaveChangesAsync();
            }
        }

        // Obtener una reserva específica por ID
        public async Task<Reservation> GetReservationByIdAsync(int id)
        {
            return await _context.Reservations.FirstOrDefaultAsync(r => r.Id == id);
        }

        // Obtener reservas por SpaceId con rango de fechas opcional
        public async Task<IEnumerable<Reservation>> GetReservationBySpaceId(int spaceId, DateTime? startDate = null, DateTime? endDate = null)
        {
            var query = _context.Reservations.AsQueryable();

            query = query.Where(r => r.SpaceId == spaceId);

            if (startDate.HasValue && endDate.HasValue)
            {
                query = query.Where(r => r.StartDate >= startDate && r.EndDate <= endDate);
            }

            return await query.ToListAsync();
        }

        // Obtener reservas por UserId con rango de fechas opcional
        public async Task<IEnumerable<Reservation>> GetReservationByUserId(int userId, DateTime? startDate = null, DateTime? endDate = null)
        {
            var query = _context.Reservations.AsQueryable();

            query = query.Where(r => r.UserId == userId);

            if (startDate.HasValue && endDate.HasValue)
            {
                query = query.Where(r => r.StartDate >= startDate && r.EndDate <= endDate);
            }

            return await query.ToListAsync();
        }

        // Obtener todas las reservas
        public async Task<IEnumerable<Reservation>> GetReservationsAsync()
        {
            return await _context.Reservations.ToListAsync();
        }

        // Validar si hay solapamientos en las reservas
        public async Task<bool> IsOverlappingAsync(int spaceId, DateTime startDate, DateTime endDate)
        {
            return await _context.Reservations.AnyAsync(r =>
                r.SpaceId == spaceId &&
                ((r.StartDate < endDate && r.EndDate > startDate)));
        }
    }
}
