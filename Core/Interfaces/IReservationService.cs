using Backend_agendamientos.Core.Entities;

namespace Backend_agendamientos.Core.Interfaces
{
    public interface IReservationService
    {
        Task<IEnumerable<Reservation>> GetAllReservationsAsync();
        Task<Reservation> CreateReservationAsync(Reservation reservation);
        Task DeleteReservationAsync(int id);
    }
}
