using Claims.BusinessLogic.Entities;
using Claims.BusinessLogic.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Claims.Database.Repositories
{
    public class CoversRepository : ICoversRepository
    {
        private readonly ClaimsContext _context;

        public CoversRepository(ClaimsContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Cover>> GetCoversAsync()
        {
            return await _context.Covers.ToListAsync();
        }

        public async Task<Cover> GetCoverAsync(string id)
        {
            return await _context.Covers.Where(cover => cover.Id == id).SingleOrDefaultAsync();
        }

        public async Task AddCoverAsync(Cover item)
        {
            _context.Covers.Add(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCoverAsync(string id)
        {
            var cover = await GetCoverAsync(id);
            if (cover is not null)
            {
                _context.Covers.Remove(cover);
                await _context.SaveChangesAsync();
            }
        }
    }
}
