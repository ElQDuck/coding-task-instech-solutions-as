using Claims.BusinessLogic.Entities;
using Claims.BusinessLogic.Interfaces;

namespace Claims.BusinessLogic.Services.Strategies;

/// <summary>
/// Premium calculation strategy for tankers.
/// </summary>
public class TankerPremiumStrategy : BaseCoverPremiumStrategy
{
    /// <summary>
    /// Initializes an instance of the <see cref="TankerPremiumStrategy"/> class.
    /// </summary>
    public TankerPremiumStrategy() : base(CoverType.Tanker) { }
    protected override decimal Multiplier => 1.5m;
}
