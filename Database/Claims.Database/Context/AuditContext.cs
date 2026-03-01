using Claims.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Claims.Database.Context
{
    /// <summary>
    /// The database context for auditing.
    /// </summary>
    public class AuditContext : Microsoft.EntityFrameworkCore.DbContext
    {
        /// <summary>
        /// Initializes an instance of the <see cref="AuditContext"/> class.
        /// </summary>
        /// <param name="options">The options for this context.</param>
        public AuditContext(DbContextOptions<AuditContext> options) : base(options)
        {
        }
        /// <summary>Gets or sets the set of claim audits.</summary>
        public DbSet<ClaimAudit> ClaimAudits { get; set; }
        /// <summary>Gets or sets the set of cover audits.</summary>
        public DbSet<CoverAudit> CoverAudits { get; set; }
    }
}
