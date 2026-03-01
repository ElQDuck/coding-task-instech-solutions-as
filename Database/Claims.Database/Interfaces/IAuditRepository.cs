using Claims.Database.Entities;

namespace Claims.Database.Auditing
{
    /// <summary>
    /// Defines the repository for managing audit records.
    /// </summary>
    public interface IAuditRepository
    {
        /// <summary>
        /// Adds a claim audit record to the database.
        /// </summary>
        /// <param name="audit">The claim audit record to add.</param>
        void AddClaimAudit(ClaimAudit audit);

        /// <summary>
        /// Adds a cover audit record to the database.
        /// </summary>
        /// <param name="audit">The cover audit record to add.</param>
        void AddCoverAudit(CoverAudit audit);
    }
}
