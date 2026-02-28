using Claims.BusinessLogic.Entities;
using Claims.BusinessLogic.Interfaces;

namespace Claims.Database.Repositories.Repositories
{
    public class ClaimsRepository : IClaimsRepository
    {
        private readonly DbContext _context;

        public ClaimsRepository(DbContext context)
        {
            _context = context;
        }

        public async Task<Result<IEnumerable<Claim>>> GetClaimsAsync()
        {
            return await _context.GetClaimsAsync();
        }

        public async Task<Result<Claim>> GetClaimAsync(string id)
        {
            return await _context.GetClaimAsync(id);
        }

        public async Task<Result<Claim>> AddClaimAsync(Claim item)
        {
            return await _context.AddItemAsync(item);
        }

        public async Task<Result> DeleteClaimAsync(string id)
        {
            return await _context.DeleteItemAsync(id);
        }
    }
}
