using Claims.BusinessLogic.Entities;

namespace Claims.BusinessLogic.Interfaces;

/// <summary>
/// Defines the service for computing premiums for covers.
/// </summary>
public interface IPremiumComputeService
{
    /// <summary>
    /// Computes the premium for a cover.
    /// </summary>
    /// <param name="startDate">The start date of the cover.</param>
    /// <param name="endDate">The end date of the cover.</param>
    /// <param name="coverType">The type of the cover.</param>
    /// <returns>The computed premium.</returns>
    decimal ComputePremium(DateTime startDate, DateTime endDate, CoverType coverType);
}