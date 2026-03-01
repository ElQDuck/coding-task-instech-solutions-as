using Claims.BusinessLogic.Entities;

namespace Claims.BusinessLogic.Interfaces
{
    /// <summary>
    /// Defines the repository for managing claims in the database.
    /// </summary>
    public interface IClaimsRepository
    {
        /// <summary>
        /// Gets all claims from the database.
        /// </summary>
        /// <returns>A list of all claims.</returns>
        Task<Result<IEnumerable<Claim>>> GetClaimsAsync();

        /// <summary>
        /// Gets a single claim by its ID.
        /// </summary>
        /// <param name="id">The claim ID.</param>
        /// <returns>The requested claim.</returns>
        Task<Result<Claim>> GetClaimAsync(string id);

        /// <summary>
        /// Adds a new claim to the database.
        /// </summary>
        /// <param name="item">The claim to add.</param>
        /// <returns>The added claim.</returns>
        Task<Result<Claim>> AddClaimAsync(Claim item);

        /// <summary>
        /// Deletes a claim from the database.
        /// </summary>
        /// <param name="id">The ID of the claim to delete.</param>
        /// <returns>A result indicating success or failure.</returns>
        Task<Result> DeleteClaimAsync(string id);
    }
}
