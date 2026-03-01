using Claims.BusinessLogic.Entities;
using Claims.BusinessLogic.Interfaces;

namespace Claims.BusinessLogic.Services.Strategies;

public class BaseCoverPremiumStrategy : ICoverPremiumStrategy
{
    private readonly IDiscountProvider _discountProvider;

    public BaseCoverPremiumStrategy(CoverType supportedType, IDiscountProvider discountProvider)
    {
        SupportedType = supportedType;
        _discountProvider = discountProvider;
    }

    public CoverType SupportedType { get; }
    protected virtual decimal Multiplier => 1.3m;

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
