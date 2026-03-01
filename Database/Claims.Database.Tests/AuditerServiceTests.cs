using Claims.Database.Services;
using Claims.Database.Auditing;
using Claims.Database.Entities;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NUnit.Framework;

namespace Claims.Database.Tests;

[Category("UnitTests")]
public class AuditerServiceTests
{
    [Test]
    public async Task AuditClaim_SendsClaimAudit()
    {
        var channel = Substitute.For<IAuditChannel>();
        var logger = Substitute.For<ILogger<AuditerService>>();
        var svc = new AuditerService(channel, logger);

        await svc.AuditClaim("cid", "POST");

        await channel.Received(1).SendAsync(Arg.Is<object>(o =>
            o != null && o.GetType() == typeof(ClaimAudit) && ((ClaimAudit)o).ClaimId == "cid" && ((ClaimAudit)o).HttpRequestType == "POST" && ((ClaimAudit)o).Created != default));
    }

    [Test]
    public async Task AuditCover_SendsCoverAudit()
    {
        var channel = Substitute.For<IAuditChannel>();
        var logger = Substitute.For<ILogger<AuditerService>>();
        var svc = new AuditerService(channel, logger);

        await svc.AuditCover("cov1", "DELETE");

        await channel.Received(1).SendAsync(Arg.Is<object>(o =>
            o != null && o.GetType() == typeof(CoverAudit) && ((CoverAudit)o).CoverId == "cov1" && ((CoverAudit)o).HttpRequestType == "DELETE" && ((CoverAudit)o).Created != default));
    }
}
