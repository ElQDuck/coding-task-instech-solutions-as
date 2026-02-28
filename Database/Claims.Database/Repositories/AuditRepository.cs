using Claims.BusinessLogic.Entities.Auditing;
using Claims.BusinessLogic.Interfaces;

namespace Claims.Database.Repositories.Repositories
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
