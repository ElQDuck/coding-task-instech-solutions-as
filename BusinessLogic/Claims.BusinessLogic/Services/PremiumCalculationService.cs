using Claims.BusinessLogic.Entities;
using Claims.BusinessLogic.Interfaces;
using Claims.BusinessLogic.Services.Strategies;

namespace Claims.BusinessLogic.Services;

public class PremiumCalculationService : IPremiumCalculationService
{
    private readonly IEnumerable<ICoverPremiumStrategy> _strategies;
    private readonly IDefaultStrategyProvider _defaultStrategyProvider;

    public PremiumCalculationService(
        IEnumerable<ICoverPremiumStrategy> strategies, 
        IDefaultStrategyProvider defaultStrategyProvider)
    {
        _strategies = strategies;
        _defaultStrategyProvider = defaultStrategyProvider;
    }

    public decimal CalculatePremium(DateTime startDate, DateTime endDate, CoverType coverType)
    {
        var strategy = _strategies.FirstOrDefault(s => s.SupportedType == coverType) 
                       ?? _defaultStrategyProvider.GetDefaultStrategy(coverType);
        
        return strategy.CalculatePremium(startDate, endDate);
    }
}
