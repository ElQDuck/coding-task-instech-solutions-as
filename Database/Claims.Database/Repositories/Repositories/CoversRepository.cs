using Claims.BusinessLogic.Entities;
using Claims.BusinessLogic.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Claims.Database.Repositories.Repositories
{
    public class CoversRepository : ICoversRepository
    {
        private readonly DbContext _context;

        public CoversRepository(DbContext context)
        {
            _context = context;
        }

        public async Task<Result<IEnumerable<Cover>>> GetCoversAsync()
        {
            return await _context.GetCoversAsync();
        }

        public async Task<Result<Cover>> GetCoverAsync(string id)
        {
            return await _context.GetCoverAsync(id);
        }

        public async Task<Result<Cover>> AddCoverAsync(Cover item)
        {
            return await _context.AddCoverAsync(item);
        }

        public async Task<Result> DeleteCoverAsync(string id)
        {
            return await _context.DeleteCoverAsync(id);
        }
    }
}
