using Claims.BusinessLogic.Entities;
using Microsoft.EntityFrameworkCore;
using MongoDB.EntityFrameworkCore.Extensions;

namespace Claims.Database.Context
{
    public class DbContext : Microsoft.EntityFrameworkCore.DbContext
    {

        public DbSet<Claim> Claims { get; init; }
        public DbSet<Cover> Covers { get; init; }

        public DbContext(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Claim>().ToCollection("claims");
            modelBuilder.Entity<Cover>().ToCollection("covers");
        }

        public async Task<Result<IEnumerable<Claim>>> GetClaimsAsync()
        {
            var claims = await Claims.ToListAsync();
            return Result.FromSuccess(claims.AsEnumerable());
        }

        public async Task<Result<IEnumerable<Cover>>> GetCoversAsync()
        {
            var covers = await Covers.ToListAsync();
            return Result.FromSuccess(covers.AsEnumerable());
        }

        public async Task<Result<Claim>> GetClaimAsync(string id)
        {
            var result =  await Claims
                .Where(claim => claim.Id == id)
                .SingleOrDefaultAsync();
            if (result is not null)
            {
                return Result.FromSuccess(result);
                
            }
            // TODO: Move messages into resources.
            var exception = new Exception($"Claim with id '{id}' not found");
            return Result.FromException<Claim>(exception);
        }

        public async Task<Result<Cover>> GetCoverAsync(string id)
        {
            var result =  await Covers
                .Where(cover => cover.Id == id)
                .SingleOrDefaultAsync();
            if (result is not null)
            {
                return Result.FromSuccess(result);
                
            }
            // TODO: Move messages into resources.
            var exception = new Exception($"Cover with id '{id}' not found");
            return Result.FromException<Cover>(exception);
        }

        public async Task<Result<Claim>> AddClaimAsync(Claim item)
        {
            Claims.Add(item);
            await SaveChangesAsync();
            return Result.FromSuccess(item);
        }

        public async Task<Result<Cover>> AddCoverAsync(Cover item)
        {
            Covers.Add(item);
            await SaveChangesAsync();
            return Result.FromSuccess(item);
        }

        public async Task<Result> DeleteClaimAsync(string id)
        {
            var result = await GetClaimAsync(id);
            if (result.IsSuccess)
            {
                Claims.Remove(result.Value);
                // TODO: Probably add try catch for better result message.
                await SaveChangesAsync();
                return Result.FromSuccess();
            }
            // TODO: Move messages into resources.
            var exception = new Exception($"Claim with id '{id}' could not be deleted.");
            return Result.FromException(exception);
        }

        public async Task<Result> DeleteCoverAsync(string id)
        {
            var result = await GetCoverAsync(id);
            if (result.IsSuccess)
            {
                Covers.Remove(result.Value);
                // TODO: Probably add try catch for better result message.
                await SaveChangesAsync();
                return Result.FromSuccess();
            }
            // TODO: Move messages into resources.
            var exception = new Exception($"Cover with id '{id}' could not be deleted.");
            return Result.FromException(exception);
        }
    }
}
