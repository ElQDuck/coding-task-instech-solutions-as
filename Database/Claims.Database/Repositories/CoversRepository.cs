using Microsoft.EntityFrameworkCore;
using Claims.BusinessLogic.Entities;
using Claims.BusinessLogic.Interfaces;
using DbContext = Claims.Database.Context.DbContext;

namespace Claims.Database.Repositories
{
    public class CoversRepository : ICoversRepository
    {
        private readonly DbContext _context;
        private readonly IAuditerService _auditer;

        public CoversRepository(DbContext context, IAuditerService auditer)
        {
            _context = context;
            _auditer = auditer;
        }

        public async Task<Result<IEnumerable<Cover>>> GetCoversAsync()
        {
            var covers = await _context.Covers.ToListAsync();
            return Result.FromSuccess(covers.AsEnumerable());
        }

        public async Task<Result<Cover>> GetCoverAsync(string id)
        {
            var result = await _context.Covers
                .Where(cover => cover.Id == id)
                .SingleOrDefaultAsync();
            if (result is not null)
            {
                return Result.FromSuccess(result);
            }
            return Result.FromException<Cover>(new Exception($"Cover with id '{id}' not found"));
        }

        public async Task<Result<Cover>> AddCoverAsync(Cover item)
        {
            _context.Covers.Add(item);
            await _context.SaveChangesAsync();
            await _auditer.AuditCover(item.Id, "POST");
            return Result.FromSuccess(item);
        }

        public async Task<Result> DeleteCoverAsync(string id)
        {
            var result = await GetCoverAsync(id);
            if (result.IsSuccess)
            {
                _context.Covers.Remove(result.Value);
                await _context.SaveChangesAsync();
                await _auditer.AuditCover(id, "DELETE");
                return Result.FromSuccess();
            }
            return Result.FromException(new Exception($"Cover with id '{id}' could not be deleted."));
        }
    }
}
