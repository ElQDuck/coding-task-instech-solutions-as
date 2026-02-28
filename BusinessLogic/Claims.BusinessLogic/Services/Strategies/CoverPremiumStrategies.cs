using Claims.BusinessLogic.Entities;
using Claims.BusinessLogic.Interfaces;

namespace Claims.BusinessLogic.Services.Strategies;

public abstract class BaseCoverPremiumStrategy : ICoverPremiumStrategy
{
    public abstract CoverType SupportedType { get; }
    protected abstract decimal Multiplier { get; }

    public virtual decimal CalculatePremium(DateTime startDate, DateTime endDate)
    {
        var premiumPerDay = 1250 * Multiplier;
        var insuranceLength = (endDate - startDate).TotalDays;
        var totalPremium = 0m;

        for (var i = 0; i < insuranceLength; i++)
        {
            totalPremium += GetDailyPremium(i);
        }

        return totalPremium;
    }

    protected virtual decimal GetDailyPremium(int dayIndex)
    {
        var premiumPerDay = 1250 * Multiplier;
        var dailyTotal = 0m;

        if (dayIndex < 30) dailyTotal += premiumPerDay;
        
        if (dayIndex < 180)
        {
            dailyTotal += premiumPerDay - (premiumPerDay * GetSecondTierDiscount());
        }
        
        if (dayIndex < 365)
        {
            dailyTotal += premiumPerDay - (premiumPerDay * GetThirdTierDiscount());
        }

        return dailyTotal;
    }

    protected virtual decimal GetSecondTierDiscount() => 0.02m;
    protected virtual decimal GetThirdTierDiscount() => 0.03m;
}

public class YachtPremiumStrategy : BaseCoverPremiumStrategy
{
    public override CoverType SupportedType => CoverType.Yacht;
    protected override decimal Multiplier => 1.1m;
    protected override decimal GetSecondTierDiscount() => 0.05m;
    protected override decimal GetThirdTierDiscount() => 0.08m;
}

public class PassengerShipPremiumStrategy : BaseCoverPremiumStrategy
{
    public override CoverType SupportedType => CoverType.PassengerShip;
    protected override decimal Multiplier => 1.2m;
}

public class TankerPremiumStrategy : BaseCoverPremiumStrategy
{
    public override CoverType SupportedType => CoverType.Tanker;
    protected override decimal Multiplier => 1.5m;
}

public class DefaultPremiumStrategy : BaseCoverPremiumStrategy
{
    private readonly CoverType _type;
    public DefaultPremiumStrategy(CoverType type) => _type = type;
    public override CoverType SupportedType => _type;
    protected override decimal Multiplier => 1.3m;
}
