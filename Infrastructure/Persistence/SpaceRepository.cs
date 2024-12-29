using Backend_agendamientos.Core.Entities;
using Backend_agendamientos.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Backend_agendamientos.Infrastructure.Persistence
{
    public class SpaceRepository : ISpaceRepository
    {
        private readonly AppDbContext _context;

        public SpaceRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Space>> GetSpacesAsync()
        {
            return await _context.Spaces.ToListAsync();
        }

        public async Task<Space> GetSpaceByIdAsync(int id)
        {
            return await _context.Spaces.FindAsync(id);
        }

        public async Task AddSpaceAsync(Space space)
        {
            _context.Spaces.Add(space);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteSpaceAsync(int id)
        {
            var space = await GetSpaceByIdAsync(id);
            if (space != null)
            {
                _context.Spaces.Remove(space);
                await _context.SaveChangesAsync();
            }
        }

        public Task UpdateSpaceAsync(Space space)
        {
            throw new NotImplementedException();
        }
    }
}
