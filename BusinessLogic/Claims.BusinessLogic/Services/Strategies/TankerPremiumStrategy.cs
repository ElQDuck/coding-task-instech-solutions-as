using Claims.BusinessLogic.Entities;
using Claims.BusinessLogic.Interfaces;

namespace Claims.BusinessLogic.Services.Strategies;

public class TankerPremiumStrategy : BaseCoverPremiumStrategy
{
    public TankerPremiumStrategy(IDiscountProvider discountProvider) : base(CoverType.Tanker, discountProvider) { }
    protected override decimal Multiplier => 1.5m;
}
