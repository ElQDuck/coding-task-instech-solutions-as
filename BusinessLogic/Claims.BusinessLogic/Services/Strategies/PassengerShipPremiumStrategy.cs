using Claims.BusinessLogic.Entities;
using Claims.BusinessLogic.Interfaces;

namespace Claims.BusinessLogic.Services.Strategies;

public class PassengerShipPremiumStrategy : BaseCoverPremiumStrategy
{
    public PassengerShipPremiumStrategy(IDiscountProvider discountProvider) : base(CoverType.PassengerShip, discountProvider) { }
    protected override decimal Multiplier => 1.2m;
}
