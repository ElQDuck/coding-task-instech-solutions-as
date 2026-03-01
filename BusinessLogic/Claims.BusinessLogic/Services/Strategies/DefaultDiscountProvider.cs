using Claims.BusinessLogic.Entities;
using Claims.BusinessLogic.Interfaces;

namespace Claims.BusinessLogic.Services.Strategies;

public class DefaultDiscountProvider : IDiscountProvider
{
    public decimal GetDiscountForDay(int dayIndex)
    {
        return dayIndex switch
        {
            // First 30 days
            < 30 => 0m,
            // Following 150 days
            < 180 => 0.02m,
            // The remaining days 2% + 1%
            _ => 0.03m
        };
    }
}
