using Claims.BusinessLogic.Entities;

namespace Claims.BusinessLogic.Interfaces
{
    /// <summary>
    /// Defines the service for managing claims.
    /// </summary>
    public interface IClaimsService
    {
        /// <summary>
        /// Provides all claims in the database.
        /// </summary>
        /// <returns>A list of all claims.</returns>
        Task<Result<IEnumerable<Claim>>> GetClaimsAsync();
        
        /// <summary>
        /// Provides a single claim for a specific ID.
        /// </summary>
        /// <param name="id">The ID of the requested claim.</param>
        /// <returns>The requested claim.</returns>
        Task<Result<Claim>> GetClaimAsync(string id);

        /// <summary>
        /// Creates a new claim.
        /// </summary>
        /// <param name="claim">The claim to create.</param>
        /// <returns>The created claim.</returns>
        Task<Result<Claim>> CreateClaimAsync(Claim claim);

        /// <summary>
        /// Deletes a claim.
        /// </summary>
        /// <param name="id">The ID of the claim to delete.</param>
        /// <returns>A result indicating success or failure.</returns>
        Task<Result> DeleteClaimAsync(string id);
    }
}
