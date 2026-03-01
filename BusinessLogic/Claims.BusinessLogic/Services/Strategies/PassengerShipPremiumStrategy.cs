using Claims.BusinessLogic.Entities;
using Claims.BusinessLogic.Interfaces;

namespace Claims.BusinessLogic.Services.Strategies;

public class PassengerShipPremiumStrategy : BaseCoverPremiumStrategy
{
    public PassengerShipPremiumStrategy() : base(CoverType.PassengerShip) { }
    protected override decimal Multiplier => 1.2m;
}
