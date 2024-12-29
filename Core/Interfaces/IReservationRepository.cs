using Backend_agendamientos.Core.Entities;

namespace Backend_agendamientos.Core.Interfaces
{
    public interface IReservationRepository
    {
        Task<IEnumerable<Reservation>> GetReservationsAsync();
        Task<Reservation> GetReservationByIdAsync(int id);
        Task<IEnumerable<Reservation>> GetReservationByUserId(int userId, DateTime? startDate = null, DateTime? endDate = null);
        Task<IEnumerable<Reservation>> GetReservationBySpaceId(int spaceId, DateTime? startDate = null, DateTime? endDate = null);
        Task AddReservationAsync(Reservation reservation);
        Task DeleteReservationAsync(int id);
        Task<bool> IsOverlappingAsync(int spaceId, DateTime startDate, DateTime endDate);
    }
}
