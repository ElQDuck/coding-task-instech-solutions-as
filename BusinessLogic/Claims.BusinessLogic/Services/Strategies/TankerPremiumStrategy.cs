using Claims.BusinessLogic.Entities;
using Claims.BusinessLogic.Interfaces;

namespace Claims.BusinessLogic.Services.Strategies;

public class TankerPremiumStrategy : BaseCoverPremiumStrategy
{
    public TankerPremiumStrategy() : base(CoverType.Tanker) { }
    protected override decimal Multiplier => 1.5m;
}
