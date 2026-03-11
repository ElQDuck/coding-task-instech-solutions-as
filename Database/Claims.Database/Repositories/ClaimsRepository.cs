using Microsoft.EntityFrameworkCore;
using Claims.BusinessLogic.Entities;
using Claims.BusinessLogic.Interfaces;
using DbContext = Claims.Database.Context.DbContext;

namespace Claims.Database.Repositories
{
    public class ClaimsRepository : IClaimsRepository
    {
        private readonly DbContext _context;
        private readonly IAuditerService _auditer;

        public ClaimsRepository(DbContext context, IAuditerService auditer)
        {
            _context = context;
            _auditer = auditer;
        }

        public async Task<Result<IEnumerable<Claim>>> GetClaimsAsync()
        {
            var claims = await _context.Claims.ToListAsync();
            return Result.FromSuccess(claims.AsEnumerable());
        }

        public async Task<Result<Claim>> GetClaimAsync(string id)
        {
            var result = await _context.Claims
                .Where(claim => claim.Id == id)
                .SingleOrDefaultAsync();
            if (result is not null)
            {
                return Result.FromSuccess(result);
            }
            return Result.FromException<Claim>(new ResultException("Not found", $"Claim with id '{id}' not found"));
        }

        public async Task<Result<Claim>> AddClaimAsync(Claim item)
        {
            _context.Claims.Add(item);
            await _context.SaveChangesAsync();
            await _auditer.AuditClaim(item.Id, "POST");
            return Result.FromSuccess(item);
        }

        public async Task<Result> DeleteClaimAsync(string id)
        {
            var result = await GetClaimAsync(id);
            if (result.IsSuccess)
            {
                _context.Claims.Remove(result.Value);
                await _context.SaveChangesAsync();
                await _auditer.AuditClaim(id, "DELETE");
                return Result.FromSuccess();
            }
            return Result.FromException(new ResultException("Not found", $"Claim with id '{id}' could not be deleted."));
        }
    }
}
