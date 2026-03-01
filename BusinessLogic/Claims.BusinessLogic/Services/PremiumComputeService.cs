using Claims.BusinessLogic.Entities;
using Claims.BusinessLogic.Interfaces;

namespace Claims.BusinessLogic.Services;

public class PremiumComputeService : IPremiumComputeService
{
    private readonly IEnumerable<ICoverPremiumStrategy> _strategies;
    private readonly IDefaultStrategyProvider _defaultStrategyProvider;

    public PremiumComputeService(
        IEnumerable<ICoverPremiumStrategy> strategies, 
        IDefaultStrategyProvider defaultStrategyProvider)
    {
        _strategies = strategies;
        _defaultStrategyProvider = defaultStrategyProvider;
    }

    public decimal ComputePremium(DateTime startDate, DateTime endDate, CoverType coverType)
    {
        var strategy = _strategies.FirstOrDefault(s => s.SupportedType == coverType) 
                       ?? _defaultStrategyProvider.GetDefaultStrategy(coverType);
        
        return strategy.CalculatePremium(startDate, endDate);
    }
}
