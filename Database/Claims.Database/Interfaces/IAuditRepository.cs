using Claims.Database.Entities;

namespace Claims.Database.Auditing
{
    public interface IAuditRepository
    {
        void AddClaimAudit(ClaimAudit audit);
        void AddCoverAudit(CoverAudit audit);
    }
}
