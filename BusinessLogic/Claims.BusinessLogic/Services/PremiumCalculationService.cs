using Claims.BusinessLogic.Entities;
using Claims.BusinessLogic.Interfaces;
using Claims.BusinessLogic.Services.Strategies;

namespace Claims.BusinessLogic.Services;

public class PremiumCalculationService : IPremiumCalculationService
{
    private readonly IEnumerable<ICoverPremiumStrategy> _strategies;

    public PremiumCalculationService(IEnumerable<ICoverPremiumStrategy> strategies)
    {
        _strategies = strategies;
    }

    public decimal CalculatePremium(DateTime startDate, DateTime endDate, CoverType coverType)
    {
        var strategy = _strategies.FirstOrDefault(s => s.SupportedType == coverType) 
                       ?? new DefaultPremiumStrategy(coverType);
        
        return strategy.CalculatePremium(startDate, endDate);
    }
}
