using Backend_agendamientos.Core.Entities;

namespace Backend_agendamientos.Core.Interfaces
{
    public interface ISpaceRepository
    {
        Task<IEnumerable<Space>> GetSpacesAsync();
        Task<Space> GetSpaceByIdAsync(int id);
        Task AddSpaceAsync(Space space);
        Task UpdateSpaceAsync(Space space);
        Task DeleteSpaceAsync(int id);
    }
}
