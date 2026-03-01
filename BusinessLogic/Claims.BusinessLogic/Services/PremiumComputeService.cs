using Claims.BusinessLogic.Entities;
using Claims.BusinessLogic.Interfaces;
using Claims.BusinessLogic.Services.Strategies;

namespace Claims.BusinessLogic.Services;

public class PremiumComputeService : IPremiumComputeService
{
    private readonly IEnumerable<ICoverPremiumStrategy> _strategies;

    public PremiumComputeService(IEnumerable<ICoverPremiumStrategy> strategies)
    {
        _strategies = strategies;
    }

    public decimal ComputePremium(DateTime startDate, DateTime endDate, CoverType coverType)
    {
        var strategy = _strategies.FirstOrDefault(s => s.SupportedType == coverType) 
                       ?? new BaseCoverPremiumStrategy(coverType);
        
        return strategy.CalculatePremium(startDate, endDate);
    }
}
