using Claims.BusinessLogic.Services;
using Claims.BusinessLogic.Services.Strategies;
using Claims.BusinessLogic.Interfaces;
using Claims.BusinessLogic.Entities;
using NUnit.Framework;

namespace Claims.BusinessLogic.Tests;

[Category("UnitTests")]
public class PremiumComputeServiceTests
{
    [Test]
    public void ComputePremium_UsesProvidedStrategy()
    {
        var strategies = new ICoverPremiumStrategy[] { new TankerPremiumStrategy() };
        var svc = new PremiumComputeService(strategies);

        var start = DateTime.UtcNow;
        var end = start; // same day -> treated as 1 day

        var result = svc.ComputePremium(start, end, CoverType.Tanker);

        // Tanker multiplier is 1.5 -> daily = 1250 * 1.5
        Assert.That(result, Is.EqualTo(1250m * 1.5m));
    }

    [Test]
    public void ComputePremium_FallsBackToBaseStrategy_WhenNoMatch()
    {
        var strategies = Array.Empty<ICoverPremiumStrategy>();
        var svc = new PremiumComputeService(strategies);

        var start = DateTime.UtcNow;
        var end = start; // 1 day

        var result = svc.ComputePremium(start, end, CoverType.Yacht);

        // Base multiplier is 1.3
        Assert.That(result, Is.EqualTo(1250m * 1.3m));
    }

    [Test]
    public void ComputePremium_Yacht_28Days()
    {
        var strategies = new ICoverPremiumStrategy[] { new YachtPremiumStrategy() };
        var svc = new PremiumComputeService(strategies);

        var start = DateTime.UtcNow;
        var end = start.AddDays(28);

        var result = svc.ComputePremium(start, end, CoverType.Yacht);

        var dailyBase = 1250m * 1.1m;
        var expected = dailyBase * 28m;

        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void ComputePremium_Yacht_42Days()
    {
        var strategies = new ICoverPremiumStrategy[] { new YachtPremiumStrategy() };
        var svc = new PremiumComputeService(strategies);

        var start = DateTime.UtcNow;
        var end = start.AddDays(42);

        var result = svc.ComputePremium(start, end, CoverType.Yacht);

        var dailyBase = 1250m * 1.1m;
        decimal expected = 0m;
        for (int i = 0; i < 42; i++)
        {
            var discount = i < 30 ? 0m : 0.05m;
            expected += dailyBase * (1 - discount);
        }

        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void ComputePremium_Yacht_300Days()
    {
        var strategies = new ICoverPremiumStrategy[] { new YachtPremiumStrategy() };
        var svc = new PremiumComputeService(strategies);

        var start = DateTime.UtcNow;
        var end = start.AddDays(300);

        var result = svc.ComputePremium(start, end, CoverType.Yacht);

        var dailyBase = 1250m * 1.1m;
        decimal expected = 0m;
        for (int i = 0; i < 300; i++)
        {
            var discount = i < 30 ? 0m : (i < 180 ? 0.05m : 0.08m);
            expected += dailyBase * (1 - discount);
        }

        Assert.That(result, Is.EqualTo(expected));
    }
}
