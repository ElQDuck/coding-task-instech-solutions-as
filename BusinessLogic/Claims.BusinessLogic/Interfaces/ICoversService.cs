using Claims.BusinessLogic.Entities;

namespace Claims.BusinessLogic.Interfaces
{
    /// <summary>
    /// Defines the service for managing covers.
    /// </summary>
    public interface ICoversService
    {
        /// <summary>
        /// Provides all covers in the database.
        /// </summary>
        /// <returns>A list of all covers.</returns>
        Task<Result<IEnumerable<Cover>>> GetCoversAsync();

        /// <summary>
        /// Provides a single cover for a specific ID.
        /// </summary>
        /// <param name="id">The ID of the requested cover.</param>
        /// <returns>The requested cover.</returns>
        Task<Result<Cover>> GetCoverAsync(string id);

        /// <summary>
        /// Creates a new cover.
        /// </summary>
        /// <param name="cover">The cover to create.</param>
        /// <returns>The created cover.</returns>
        Task<Result<Cover>> CreateCoverAsync(Cover cover);

        /// <summary>
        /// Deletes a cover.
        /// </summary>
        /// <param name="id">The ID of the cover to delete.</param>
        /// <returns>A result indicating success or failure.</returns>
        Task<Result> DeleteCoverAsync(string id);
    }
}
