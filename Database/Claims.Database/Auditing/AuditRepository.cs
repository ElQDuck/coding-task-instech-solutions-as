using Claims.Database.Context;
using Claims.Database.Entities;

namespace Claims.Database.Auditing
{
    public class AuditRepository : IAuditRepository
    {
        private readonly AuditContext _auditContext;

        public AuditRepository(AuditContext auditContext)
        {
            _auditContext = auditContext;
        }

        public void AddClaimAudit(ClaimAudit audit)
        {
            _auditContext.Add(audit);
            _auditContext.SaveChanges();
        }

        public void AddCoverAudit(CoverAudit audit)
        {
            _auditContext.Add(audit);
            _auditContext.SaveChanges();
        }
    }
}
