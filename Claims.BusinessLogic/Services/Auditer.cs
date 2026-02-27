using Claims.BusinessLogic.Entities.Auditing;
using Claims.BusinessLogic.Interfaces;

namespace Claims.BusinessLogic.Services
{
    public class Auditer
    {
        private readonly IAuditRepository _auditRepository;

        public Auditer(IAuditRepository auditRepository)
        {
            _auditRepository = auditRepository;
        }

        public void AuditClaim(string id, string httpRequestType)
        {
            var claimAudit = new ClaimAudit()
            {
                Created = DateTime.Now,
                HttpRequestType = httpRequestType,
                ClaimId = id
            };

            _auditRepository.AddClaimAudit(claimAudit);
        }
        
        public void AuditCover(string id, string httpRequestType)
        {
            var coverAudit = new CoverAudit()
            {
                Created = DateTime.Now,
                HttpRequestType = httpRequestType,
                CoverId = id
            };

            _auditRepository.AddCoverAudit(coverAudit);
        }
    }
}
