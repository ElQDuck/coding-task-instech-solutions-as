using Claims.BusinessLogic.Entities;
using Claims.BusinessLogic.Interfaces;

namespace Claims.BusinessLogic.Services.Strategies;

/// <summary>
/// The 
/// </summary>
public class YachtPremiumStrategy : BaseCoverPremiumStrategy
{
    public YachtPremiumStrategy() : base(CoverType.Yacht) { }
    protected override decimal Multiplier => 1.1m;

    protected override decimal GetDiscountForDay(int dayIndex)
    {
        return dayIndex switch
        {
            // First 30 days
            < 30 => 0m,
            // Following 150 days
            < 180 => 0.05m,
            // The remaining days 5% + 3%
            _ => 0.08m
        };
    }
}
