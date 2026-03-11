using Claims.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

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
    
    public class AuditContextFactory : IDesignTimeDbContextFactory<AuditContext>
    {
        public AuditContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AuditContext>();
            // Use a dummy connection string; it just needs to know the provider (SQL Server)
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=ClaimsAudit;Trusted_Connection=True;");

            return new AuditContext(optionsBuilder.Options);
        }
    }
}
