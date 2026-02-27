using Claims.BusinessLogic.Entities.Auditing;

namespace Claims.BusinessLogic.Interfaces
{
    public interface IAuditRepository
    {
        void AddClaimAudit(ClaimAudit audit);
        void AddCoverAudit(CoverAudit audit);
    }
}
