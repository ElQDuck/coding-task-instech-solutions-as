using Claims.BusinessLogic.Entities;

namespace Claims.BusinessLogic.Interfaces;

/// <summary>
/// Defines a strategy for calculating premiums for a specific cover type.
/// </summary>
public interface ICoverPremiumStrategy
{
    /// <summary>
    /// Gets the cover type supported by this strategy.
    /// </summary>
    CoverType SupportedType { get; }

    /// <summary>
    /// Calculates the premium for a cover within a given date range.
    /// </summary>
    /// <param name="startDate">The start date of the cover.</param>
    /// <param name="endDate">The end date of the cover.</param>
    /// <returns>The calculated premium.</returns>
    decimal CalculatePremium(DateTime startDate, DateTime endDate);
}
