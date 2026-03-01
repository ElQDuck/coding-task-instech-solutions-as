using Claims.BusinessLogic.Entities;
using Claims.BusinessLogic.Interfaces;

namespace Claims.BusinessLogic.Services.Strategies;

public class DefaultDiscountProvider : IDiscountProvider
{
    public IEnumerable<decimal> GetDiscounts(CoverType type, int dayIndex)
    {
        var discounts = new List<decimal>();

        switch (dayIndex)
        {
            // First 30 days => no discount
            case < 30:
                discounts.Add(0m);
                break;
            // Following 150 days are discounted by 2%
            case < 180:
                discounts.Add(0.02m);
                break;
            // The remaining days are discounted by additional 1%
            default:
                discounts.Add(0.03m);
                break;
        }

        return discounts;
    }
}
