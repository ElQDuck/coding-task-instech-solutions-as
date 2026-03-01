using Claims.BusinessLogic.Entities;
using Claims.BusinessLogic.Interfaces;
using Claims.BusinessLogic.Services.Strategies;

namespace Claims.BusinessLogic.Services;

public class PremiumComputeService : IPremiumComputeService
{
    private readonly IEnumerable<ICoverPremiumStrategy> _strategies;
    private readonly IDiscountProvider _discountProvider;

    public PremiumComputeService(
        IEnumerable<ICoverPremiumStrategy> strategies, 
        IDiscountProvider discountProvider)
    {
        _strategies = strategies;
        _discountProvider = discountProvider;
    }

    public decimal ComputePremium(DateTime startDate, DateTime endDate, CoverType coverType)
    {
        var strategy = _strategies.FirstOrDefault(s => s.SupportedType == coverType) 
                       ?? new BaseCoverPremiumStrategy(coverType, _discountProvider);
        
        return strategy.CalculatePremium(startDate, endDate);
    }
}
