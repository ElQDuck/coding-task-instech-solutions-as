using Claims.BusinessLogic.Interfaces;
using Claims.Database.Auditing;
using Claims.Database.Entities;
using Microsoft.Extensions.Logging;

namespace Claims.Database.Services
{
    public class AuditerService : IAuditerService
    {
        private readonly IAuditChannel _auditChannel;
        private readonly ILogger<AuditerService> _logger;

        public AuditerService(IAuditChannel auditChannel, ILogger<AuditerService> logger)
        {
            _auditChannel = auditChannel;
            _logger = logger;
        }

        public async Task AuditClaim(string id, string httpRequestType)
        {
            _logger.LogDebug("AuditerService AuditClaim IAuditChannel InstanceId={id}", _auditChannel.GetHashCode());
            var claimAudit = new ClaimAudit()
            {
                Created = DateTime.Now,
                HttpRequestType = httpRequestType,
                ClaimId = id
            };

            await _auditChannel.SendAsync(claimAudit);
            _logger.LogDebug("AuditerService AuditClaim SendAsync completed. IAuditChannel InstanceId={id}", _auditChannel.GetHashCode());
        }
        
        public async Task AuditCover(string id, string httpRequestType)
        {
            _logger.LogDebug("AuditerService AuditCover IAuditChannel InstanceId={id}", _auditChannel.GetHashCode());
            var coverAudit = new CoverAudit()
            {
                Created = DateTime.Now,
                HttpRequestType = httpRequestType,
                CoverId = id
            };

            await _auditChannel.SendAsync(coverAudit);
            _logger.LogDebug("AuditerService AuditCover SendAsync completed. IAuditChannel InstanceId={id}", _auditChannel.GetHashCode());
        }
    }
}
