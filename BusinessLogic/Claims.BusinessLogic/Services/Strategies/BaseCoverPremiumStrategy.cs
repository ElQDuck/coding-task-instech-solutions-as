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
    private const decimal BaseRate = 1250m;

    public virtual decimal CalculatePremium(DateTime startDate, DateTime endDate)
    {
        // Calculating only full days
        var insuranceLengthTotalDays = (endDate.Date - startDate.Date).Days;
        var dailyBase = BaseRate * Multiplier;
        var totalPremium = 0m;

        for (var i = 0; i < insuranceLengthTotalDays; i++)
        {
            totalPremium += dailyBase * (1 - _discountProvider.GetDiscountForDay(i));
        }

        return totalPremium;
    }
}
