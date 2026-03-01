using Claims.BusinessLogic.Entities;

namespace Claims.BusinessLogic.Interfaces
{
    public interface IClaimsService
    {
        /// <summary>
        /// Provides all claims in the database.
        /// </summary>
        Task<Result<IEnumerable<Claim>>> GetClaimsAsync();
        
        /// <summary>
        /// Provides a single for a specific ID.
        /// </summary>
        /// <param name="id">The ID of the requested parameter.</param>
        /// <returns>The requested claim.</returns>
        Task<Result<Claim>> GetClaimAsync(string id);
        Task<Result<Claim>> CreateClaimAsync(Claim claim);
        Task<Result> DeleteClaimAsync(string id);
    }
}
