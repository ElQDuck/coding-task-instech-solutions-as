using Claims.BusinessLogic.Entities;
using Claims.BusinessLogic.Interfaces;

namespace Claims.BusinessLogic.Services.Strategies;

public class YachtDiscountProvider : IDiscountProvider
{
    public IEnumerable<decimal> GetDiscounts(CoverType type, int dayIndex)
    {
        var discounts = new List<decimal>();

        if (dayIndex < 30)
        {
            discounts.Add(0m);
        }

        if (dayIndex < 180)
        {
            discounts.Add(0.05m);
        }

        if (dayIndex < 365)
        {
            discounts.Add(0.08m);
        }

        return discounts;
    }
}
