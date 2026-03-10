using Claims.BusinessLogic.Services;
using Claims.BusinessLogic.Entities;
using Claims.BusinessLogic.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace Claims.BusinessLogic.Tests;

[Category("UnitTests")]
public class CoversServiceTests
{
    private ICoversRepository _repo = null!;
    private IPremiumComputeService _premium = null!;
    private CoversService _svc = null!;

    [SetUp]
    public void Setup()
    {
        _repo = Substitute.For<ICoversRepository>();
        _premium = Substitute.For<IPremiumComputeService>();
        _svc = new CoversService(_repo, _premium);
    }

    [Test]
    public async Task GetCoversAsync_DelegatesToRepository()
    {
        _repo.GetCoversAsync().Returns(Task.FromResult(Result.FromSuccess(Enumerable.Empty<Cover>())));

        var result = await _svc.GetCoversAsync();

        Assert.That(result.IsSuccess, Is.True);
    }

    [Test]
    public async Task CreateCoverAsync_Success()
    {
        var cover = new Cover
        {
            Id = "cov-success",
            StartDate = DateTime.UtcNow.AddMinutes(1),
            EndDate = DateTime.UtcNow.AddMinutes(1).AddDays(10),
            Type = CoverType.Tanker
        };
        _premium.ComputePremium(cover.StartDate, cover.EndDate, cover.Type).Returns(99.9m);
        _repo.AddCoverAsync(Arg.Any<Cover>()).Returns(Task.FromResult(Result.FromSuccess(cover)));

        var result = await _svc.CreateCoverAsync(cover);

        Assert.That(result.IsSuccess, Is.True);
        Assert.That(result.Value.Premium, Is.EqualTo(99.9m));
        Assert.That(result.Value.Id, Is.Not.Null.And.Not.Empty);
    }

    [Test]
    public async Task CreateCoverAsync_Fails_WhenStartDateInPast()
    {
        var cover = new Cover { Id = "cov-past", StartDate = DateTime.UtcNow.AddDays(-1), EndDate = DateTime.UtcNow.AddDays(1) };

        var result = await _svc.CreateCoverAsync(cover);

        Assert.That(result.IsSuccess, Is.False);
    }

    [Test]
    public async Task CreateCoverAsync_Fails_WhenEndDateExceedsOneYear()
    {
        var cover = new Cover { Id = "cov-too-long", StartDate = DateTime.UtcNow, EndDate = DateTime.UtcNow.AddYears(2) };

        var result = await _svc.CreateCoverAsync(cover);

        Assert.That(result.IsSuccess, Is.False);
    }

    [Test]
    public async Task CreateCoverAsync_Fails_WhenStartAfterEnd()
    {
        var cover = new Cover { Id = "cov-bad", StartDate = DateTime.UtcNow.AddDays(5), EndDate = DateTime.UtcNow };

        var result = await _svc.CreateCoverAsync(cover);

        Assert.That(result.IsSuccess, Is.False);
    }
}
