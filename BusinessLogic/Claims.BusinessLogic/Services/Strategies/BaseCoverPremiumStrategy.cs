using Claims.BusinessLogic.Entities;
using Claims.BusinessLogic.Interfaces;

namespace Claims.BusinessLogic.Services.Strategies;

/// <summary>
/// Provides a base implementation for premium calculation strategies.
/// </summary>
public class BaseCoverPremiumStrategy : ICoverPremiumStrategy
{
    /// <summary>
    /// Initializes an instance of the <see cref="BaseCoverPremiumStrategy"/> class.
    /// </summary>
    /// <param name="supportedType">The cover type supported by this strategy.</param>
    public BaseCoverPremiumStrategy(CoverType supportedType)
    {
        SupportedType = supportedType;
    }

    /// <inheritdoc/>
    public CoverType SupportedType { get; }
    protected virtual decimal Multiplier => 1.3m;
    private const decimal BaseRate = 1250m;

    /// <inheritdoc/>
    public virtual decimal CalculatePremium(DateTime startDate, DateTime endDate)
    {
        // Calculating only full days
        var insuranceLengthTotalDays = (endDate.Date - startDate.Date).Days;
        // Make sure that a 1-Day insurance is not free ;)
        if (endDate.Date == startDate.Date)
        {
            insuranceLengthTotalDays = 1;
        }
        var dailyBase = BaseRate * Multiplier;
        var totalPremium = 0m;

        for (var i = 0; i < insuranceLengthTotalDays; i++)
        {
            totalPremium += dailyBase * (1 - GetDiscountForDay(i));
        }

        return totalPremium;
    }

    protected virtual decimal GetDiscountForDay(int dayIndex)
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
