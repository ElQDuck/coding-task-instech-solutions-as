using Claims.BusinessLogic.Services;
using Claims.BusinessLogic.Entities;
using Claims.BusinessLogic.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace Claims.BusinessLogic.Tests;

[Category("UnitTests")]
public class ClaimServiceTests
{
    private IClaimsRepository _repo;
    private ICoversService _coversService;
    private ClaimsService _svc;

    [SetUp]
    public void Setup()
    {
        _repo = Substitute.For<IClaimsRepository>();
        _coversService = Substitute.For<ICoversService>();
        _svc = new ClaimsService(_repo, _coversService);
    }

    [Test]
    public async Task CreateClaimAsync_Success()
    {
        var cover = new Cover { Id = "cov1", StartDate = DateTime.UtcNow.AddDays(-1), EndDate = DateTime.UtcNow.AddDays(10) };
        var claim = new Claim { CoverId = "cov1", Created = DateTime.UtcNow, DamageCost = 10 };

        _coversService.GetCoverAsync("cov1").Returns(Task.FromResult(Result.FromSuccess(cover)));
        _repo.AddClaimAsync(Arg.Any<Claim>()).Returns(Task.FromResult(Result.FromSuccess(claim)));

        var result = await _svc.CreateClaimAsync(claim);

        Assert.That(result.IsSuccess, Is.True);
        Assert.That(result.Value.Id, Is.Not.Null.And.Not.Empty);
    }

    [Test]
    public async Task CreateClaimAsync_Fails_WhenDamageTooHigh()
    {
        var cover = new Cover { Id = "cov1", StartDate = DateTime.UtcNow.AddDays(-1), EndDate = DateTime.UtcNow.AddDays(10) };
        var claim = new Claim { CoverId = "cov1", Created = DateTime.UtcNow, DamageCost = 200000 };

        _coversService.GetCoverAsync("cov1").Returns(Task.FromResult(Result.FromSuccess(cover)));

        var result = await _svc.CreateClaimAsync(claim);

        Assert.That(result.IsSuccess, Is.False);
    }

    [Test]
    public async Task CreateClaimAsync_Fails_WhenCreatedOutsideCoverPeriod()
    {
        var cover = new Cover { Id = "cov1", StartDate = DateTime.UtcNow.AddDays(5), EndDate = DateTime.UtcNow.AddDays(10) };
        var claim = new Claim { CoverId = "cov1", Created = DateTime.UtcNow, DamageCost = 10 };

        _coversService.GetCoverAsync("cov1").Returns(Task.FromResult(Result.FromSuccess(cover)));

        var result = await _svc.CreateClaimAsync(claim);

        Assert.That(result.IsSuccess, Is.False);
    }
}
