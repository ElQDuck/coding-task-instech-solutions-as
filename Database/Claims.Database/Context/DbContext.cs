using Claims.BusinessLogic.Entities;
using Microsoft.EntityFrameworkCore;
using MongoDB.EntityFrameworkCore.Extensions;

namespace Claims.Database.Context
{
    /// <summary>
    /// The main database context for the application.
    /// </summary>
    public class DbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        /// <summary>Gets or sets the set of claims.</summary>
        public DbSet<Claim> Claims { get; init; }
        /// <summary>Gets or sets the set of covers.</summary>
        public DbSet<Cover> Covers { get; init; }

        /// <summary>
        /// Initializes an instance of the <see cref="DbContext"/> class.
        /// </summary>
        /// <param name="options">The options for this context.</param>
        public DbContext(DbContextOptions options)
            : base(options)
        {
        }

        /// <inheritdoc/>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Claim>().ToCollection("claims");
            modelBuilder.Entity<Cover>().ToCollection("covers");
        }

        /// <summary>
        /// Gets all claims from the database.
        /// </summary>
        /// <returns>A list of all claims.</returns>
        public async Task<Result<IEnumerable<Claim>>> GetClaimsAsync()
        {
            var claims = await Claims.ToListAsync();
            return Result.FromSuccess(claims.AsEnumerable());
        }

        /// <summary>
        /// Gets all covers from the database.
        /// </summary>
        /// <returns>A list of all covers.</returns>
        public async Task<Result<IEnumerable<Cover>>> GetCoversAsync()
        {
            var covers = await Covers.ToListAsync();
            return Result.FromSuccess(covers.AsEnumerable());
        }

        /// <summary>
        /// Gets a single claim by its ID.
        /// </summary>
        /// <param name="id">The claim ID.</param>
        /// <returns>The requested claim.</returns>
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

        /// <summary>
        /// Gets a single cover by its ID.
        /// </summary>
        /// <param name="id">The cover ID.</param>
        /// <returns>The requested cover.</returns>
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

        /// <summary>
        /// Adds a new claim to the database.
        /// </summary>
        /// <param name="item">The claim to add.</param>
        /// <returns>The added claim.</returns>
        public async Task<Result<Claim>> AddClaimAsync(Claim item)
        {
            Claims.Add(item);
            await SaveChangesAsync();
            return Result.FromSuccess(item);
        }

        /// <summary>
        /// Adds a new cover to the database.
        /// </summary>
        /// <param name="item">The cover to add.</param>
        /// <returns>The added cover.</returns>
        public async Task<Result<Cover>> AddCoverAsync(Cover item)
        {
            Covers.Add(item);
            await SaveChangesAsync();
            return Result.FromSuccess(item);
        }

        /// <summary>
        /// Deletes a claim from the database.
        /// </summary>
        /// <param name="id">The ID of the claim to delete.</param>
        /// <returns>A result indicating success or failure.</returns>
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

        /// <summary>
        /// Deletes a cover from the database.
        /// </summary>
        /// <param name="id">The ID of the cover to delete.</param>
        /// <returns>A result indicating success or failure.</returns>
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
