using Claims.BusinessLogic.Entities;
using Claims.BusinessLogic.Interfaces;
using Claims.BusinessLogic.Services.Strategies;

namespace Claims.BusinessLogic.Services;

/// <summary>
/// Implements the service for computing premiums using available strategies.
/// </summary>
public class PremiumComputeService : IPremiumComputeService
{
    private readonly IEnumerable<ICoverPremiumStrategy> _strategies;

    /// <summary>
    /// Initializes an instance of the <see cref="PremiumComputeService"/> class.
    /// </summary>
    /// <param name="strategies">The collection of premium calculation strategies.</param>
    public PremiumComputeService(IEnumerable<ICoverPremiumStrategy> strategies)
    {
        _strategies = strategies;
    }

    /// <inheritdoc/>
    public decimal ComputePremium(DateTime startDate, DateTime endDate, CoverType coverType)
    {
        var strategy = _strategies.FirstOrDefault(s => s.SupportedType == coverType) 
                       ?? new BaseCoverPremiumStrategy(coverType);
        
        return strategy.CalculatePremium(startDate, endDate);
    }
}
