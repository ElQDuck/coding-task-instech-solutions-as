using Claims.BusinessLogic.Entities;
using Claims.BusinessLogic.Interfaces;

namespace Claims.BusinessLogic.Services;

public class DefaultDiscountProvider : IDiscountProvider
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
            discounts.Add(type == CoverType.Yacht ? 0.05m : 0.02m);
        }

        if (dayIndex < 365)
        {
            discounts.Add(type == CoverType.Yacht ? 0.08m : 0.03m);
        }

        return discounts;
    }
}
