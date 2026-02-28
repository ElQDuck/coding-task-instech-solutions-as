using Claims.BusinessLogic.Interfaces;
using Claims.Database.Auditing;
using Claims.Database.Entities;

namespace Claims.Database.Services
{
    public class AuditerService : IAuditerService
    {
        private readonly IAuditChannel _auditChannel;

        public AuditerService(IAuditChannel auditChannel)
        {
            _auditChannel = auditChannel;
        }

        public void AuditClaim(string id, string httpRequestType)
        {
            var claimAudit = new ClaimAudit()
            {
                Created = DateTime.Now,
                HttpRequestType = httpRequestType,
                ClaimId = id
            };

            _auditChannel.SendAsync(claimAudit);
        }
        
        public void AuditCover(string id, string httpRequestType)
        {
            var coverAudit = new CoverAudit()
            {
                Created = DateTime.Now,
                HttpRequestType = httpRequestType,
                CoverId = id
            };

            _auditChannel.SendAsync(coverAudit);
        }
    }
}
