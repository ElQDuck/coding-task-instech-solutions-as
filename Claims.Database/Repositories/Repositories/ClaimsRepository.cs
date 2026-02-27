using Claims.BusinessLogic.Entities;
using Claims.BusinessLogic.Interfaces;

namespace Claims.Database.Repositories.Repositories
{
    public class ClaimsRepository : IClaimsRepository
    {
        private readonly ClaimsContext _context;

        public ClaimsRepository(ClaimsContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Claim>> GetClaimsAsync()
        {
            return await _context.GetClaimsAsync();
        }

        public async Task<Claim> GetClaimAsync(string id)
        {
            return await _context.GetClaimAsync(id);
        }

        public async Task AddClaimAsync(Claim item)
        {
            await _context.AddItemAsync(item);
        }

        public async Task DeleteClaimAsync(string id)
        {
            await _context.DeleteItemAsync(id);
        }
    }
}
