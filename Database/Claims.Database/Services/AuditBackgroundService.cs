using Claims.Database.Auditing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Claims.Database.Entities;

namespace Claims.Database.Services
{
    public class AuditBackgroundService : BackgroundService
    {
        private readonly IAuditChannel _auditChannel;
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<AuditBackgroundService> _logger;

        public AuditBackgroundService(
            IAuditChannel auditChannel, 
            IServiceProvider serviceProvider, 
            ILogger<AuditBackgroundService> logger)
        {
            _auditChannel = auditChannel;
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Audit Background Service is starting.");

            await foreach (var message in _auditChannel.ReadAllAsync().WithCancellation(stoppingToken))
            {
                try
                {
                    await ProcessAuditMessageAsync(message);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error processing audit message.");
                }
            }

            _logger.LogInformation("Audit Background Service is stopping.");
        }

        private async Task ProcessAuditMessageAsync(object message)
        {
            using var scope = _serviceProvider.CreateScope();
            var auditRepository = scope.ServiceProvider.GetRequiredService<IAuditRepository>();

            switch (message)
            {
                case ClaimAudit claimAudit:
                    auditRepository.AddClaimAudit(claimAudit);
                    break;
                case CoverAudit coverAudit:
                    auditRepository.AddCoverAudit(coverAudit);
                    break;
            }
        }
    }
}
