using Claims.BusinessLogic.Entities;

namespace Claims.BusinessLogic.Interfaces
{
    /// <summary>
    /// Defines the repository for managing covers in the database.
    /// </summary>
    public interface ICoversRepository
    {
        /// <summary>
        /// Gets all covers from the database.
        /// </summary>
        /// <returns>A list of all covers.</returns>
        Task<Result<IEnumerable<Cover>>> GetCoversAsync();

        /// <summary>
        /// Gets a single cover by its ID.
        /// </summary>
        /// <param name="id">The cover ID.</param>
        /// <returns>The requested cover.</returns>
        Task<Result<Cover>> GetCoverAsync(string id);

        /// <summary>
        /// Adds a new cover to the database.
        /// </summary>
        /// <param name="item">The cover to add.</param>
        /// <returns>The added cover.</returns>
        Task<Result<Cover>> AddCoverAsync(Cover item);

        /// <summary>
        /// Deletes a cover from the database.
        /// </summary>
        /// <param name="id">The ID of the cover to delete.</param>
        /// <returns>A result indicating success or failure.</returns>
        Task<Result> DeleteCoverAsync(string id);
    }
}
