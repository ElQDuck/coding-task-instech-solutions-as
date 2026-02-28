using Claims.BusinessLogic.Entities;
using Claims.BusinessLogic.Interfaces;

namespace Claims.BusinessLogic.Services.Strategies;

public abstract class BaseCoverPremiumStrategy : ICoverPremiumStrategy
{
    private readonly IDiscountProvider _discountProvider;

    protected BaseCoverPremiumStrategy(IDiscountProvider discountProvider)
    {
        _discountProvider = discountProvider;
    }

    public abstract CoverType SupportedType { get; }
    protected abstract decimal Multiplier { get; }

    public virtual decimal CalculatePremium(DateTime startDate, DateTime endDate)
    {
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
        var discounts = _discountProvider.GetDiscounts(SupportedType, dayIndex);
        
        var dailyTotal = 0m;
        foreach (var discount in discounts)
        {
            dailyTotal += premiumPerDay - (premiumPerDay * discount);
        }

        return dailyTotal;
    }
}

public class YachtPremiumStrategy : BaseCoverPremiumStrategy
{
    public YachtPremiumStrategy(IDiscountProvider discountProvider) : base(discountProvider) { }
    public override CoverType SupportedType => CoverType.Yacht;
    protected override decimal Multiplier => 1.1m;
}

public class PassengerShipPremiumStrategy : BaseCoverPremiumStrategy
{
    public PassengerShipPremiumStrategy(IDiscountProvider discountProvider) : base(discountProvider) { }
    public override CoverType SupportedType => CoverType.PassengerShip;
    protected override decimal Multiplier => 1.2m;
}

public class TankerPremiumStrategy : BaseCoverPremiumStrategy
{
    public TankerPremiumStrategy(IDiscountProvider discountProvider) : base(discountProvider) { }
    public override CoverType SupportedType => CoverType.Tanker;
    protected override decimal Multiplier => 1.5m;
}

public class DefaultPremiumStrategy : BaseCoverPremiumStrategy
{
    private readonly CoverType _type;
    public DefaultPremiumStrategy(CoverType type, IDiscountProvider discountProvider) : base(discountProvider) => _type = type;
    public override CoverType SupportedType => _type;
    protected override decimal Multiplier => 1.3m;
}
