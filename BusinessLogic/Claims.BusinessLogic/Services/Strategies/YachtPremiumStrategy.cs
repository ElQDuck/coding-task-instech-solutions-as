using Claims.BusinessLogic.Entities;
using Claims.BusinessLogic.Interfaces;

namespace Claims.BusinessLogic.Services.Strategies;

/// <summary>
/// The 
/// </summary>
public class YachtPremiumStrategy : BaseCoverPremiumStrategy
{
    public YachtPremiumStrategy(IDiscountProvider discountProvider) : base(CoverType.Yacht, discountProvider) { }
    protected override decimal Multiplier => 1.1m;
}
