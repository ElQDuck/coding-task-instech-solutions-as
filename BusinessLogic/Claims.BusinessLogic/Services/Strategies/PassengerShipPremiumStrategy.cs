using Claims.BusinessLogic.Entities;
using Claims.BusinessLogic.Interfaces;

namespace Claims.BusinessLogic.Services.Strategies;

/// <summary>
/// Premium calculation strategy for passenger ships.
/// </summary>
public class PassengerShipPremiumStrategy : BaseCoverPremiumStrategy
{
    /// <summary>
    /// Initializes an instance of the <see cref="PassengerShipPremiumStrategy"/> class.
    /// </summary>
    public PassengerShipPremiumStrategy() : base(CoverType.PassengerShip) { }
    protected override decimal Multiplier => 1.2m;
}
