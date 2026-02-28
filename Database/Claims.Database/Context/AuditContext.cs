using Claims.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Claims.Database.Context
{
    public class AuditContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public AuditContext(DbContextOptions<AuditContext> options) : base(options)
        {
        }
        public DbSet<ClaimAudit> ClaimAudits { get; set; }
        public DbSet<CoverAudit> CoverAudits { get; set; }
    }
}
