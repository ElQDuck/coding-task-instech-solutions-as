using Claims.Database.Context;
using Claims.Database.Entities;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Claims.Database.Auditing
{
    /// <summary>
    /// Implements the repository for storing audit records.
    /// </summary>
    public class AuditRepository : IAuditRepository
    {
        private readonly AuditContext _auditContext;
        private readonly ILogger<AuditRepository> _logger;

        /// <summary>
        /// Initializes an instance of the <see cref="AuditRepository"/> class.
        /// </summary>
        /// <param name="auditContext">The audit database context.</param>
        /// <param name="logger">The logger.</param>
        public AuditRepository(AuditContext auditContext, ILogger<AuditRepository> logger)
        {
            _auditContext = auditContext;
            _logger = logger;
        }

        /// <inheritdoc/>
        public async Task AddClaimAuditAsync(ClaimAudit audit)
        {
            _logger.LogDebug("AddClaimAuditAsync start");
            _auditContext.Add(audit);
            await _auditContext.SaveChangesAsync();
            _logger.LogDebug("AddClaimAuditAsync stop");
        }

        /// <inheritdoc/>
        public async Task AddCoverAuditAsync(CoverAudit audit)
        {
            _logger.LogDebug("AddCoverAuditAsync start");
            _auditContext.Add(audit);
            await _auditContext.SaveChangesAsync();
            _logger.LogDebug("AddCoverAuditAsync stop");
        }
    }
}
