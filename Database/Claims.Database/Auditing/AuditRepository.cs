using Claims.Database.Context;
using Claims.Database.Entities;

namespace Claims.Database.Auditing
{
    /// <summary>
    /// Implements the repository for storing audit records.
    /// </summary>
    public class AuditRepository : IAuditRepository
    {
        private readonly AuditContext _auditContext;

        /// <summary>
        /// Initializes an instance of the <see cref="AuditRepository"/> class.
        /// </summary>
        /// <param name="auditContext">The audit database context.</param>
        public AuditRepository(AuditContext auditContext)
        {
            _auditContext = auditContext;
        }

        /// <inheritdoc/>
        public void AddClaimAudit(ClaimAudit audit)
        {
            _auditContext.Add(audit);
            _auditContext.SaveChanges();
        }

        /// <inheritdoc/>
        public void AddCoverAudit(CoverAudit audit)
        {
            _auditContext.Add(audit);
            _auditContext.SaveChanges();
        }
    }
}
