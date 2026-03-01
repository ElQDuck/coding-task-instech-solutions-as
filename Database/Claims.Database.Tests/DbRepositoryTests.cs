using Claims.Database.Context;
using Claims.Database.Auditing;
using Claims.Database.Entities;
using Claims.Database.Repositories;
using Claims.BusinessLogic.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NUnit.Framework;

namespace Claims.Database.Tests;

[Category("UnitTests")]
public class DbRepositoryTests
{
    private DbContextOptions<AuditContext> _auditOptions;
    private DbContextOptions<Claims.Database.Context.DbContext> _options;

    [SetUp]
    public void Setup()
    {
        _auditOptions = new DbContextOptionsBuilder<AuditContext>()
            .UseInMemoryDatabase(databaseName: "audit_db")
            .Options;

        _options = new DbContextOptionsBuilder<Claims.Database.Context.DbContext>()
            .UseInMemoryDatabase(databaseName: "main_db")
            .Options;
    }

    [Test]
    public async Task AuditRepository_AddClaimAudit_Persists()
    {
        await using var ctx = new AuditContext(_auditOptions);
        var logger = Substitute.For<ILogger<AuditRepository>>();
        var repo = new AuditRepository(ctx, logger);

        var audit = new ClaimAudit { ClaimId = "c1", Created = DateTime.UtcNow, HttpRequestType = "POST" };
        await repo.AddClaimAuditAsync(audit);

        var saved = await ctx.ClaimAudits.SingleAsync();
        Assert.That(saved.ClaimId, Is.EqualTo("c1"));
    }

    [Test]
    public async Task DbContext_AddGetDelete_Claim_Works()
    {
        await using var ctx = new Claims.Database.Context.DbContext(_options);

        var claim = new Claim { Id = "cid", Name = "n", CoverId = "cov", Created = DateTime.UtcNow, DamageCost = 10 };
        var addRes = await ctx.AddClaimAsync(claim);
        Assert.That(addRes.IsSuccess, Is.True);

        var getRes = await ctx.GetClaimAsync("cid");
        Assert.That(getRes.IsSuccess, Is.True);
        Assert.That(getRes.Value.Id, Is.EqualTo("cid"));

        var delRes = await ctx.DeleteClaimAsync("cid");
        Assert.That(delRes.IsSuccess, Is.True);

        var getAfter = await ctx.GetClaimAsync("cid");
        Assert.That(getAfter.IsSuccess, Is.False);
    }

    [Test]
    public async Task DbContext_AddGetDelete_Cover_Works()
    {
        await using var ctx = new Claims.Database.Context.DbContext(_options);

        var cover = new Cover { Id = "cov1", StartDate = DateTime.UtcNow, EndDate = DateTime.UtcNow.AddDays(1), Premium = 1m, Type = Claims.BusinessLogic.Entities.CoverType.Tanker };
        var addRes = await ctx.AddCoverAsync(cover);
        Assert.That(addRes.IsSuccess, Is.True);

        var getRes = await ctx.GetCoverAsync("cov1");
        Assert.That(getRes.IsSuccess, Is.True);
        Assert.That(getRes.Value.Id, Is.EqualTo("cov1"));

        var delRes = await ctx.DeleteCoverAsync("cov1");
        Assert.That(delRes.IsSuccess, Is.True);

        var getAfter = await ctx.GetCoverAsync("cov1");
        Assert.That(getAfter.IsSuccess, Is.False);
    }
}
