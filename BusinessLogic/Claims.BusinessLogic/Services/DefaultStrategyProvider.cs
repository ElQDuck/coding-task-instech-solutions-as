using Claims.BusinessLogic.Entities;
using Claims.BusinessLogic.Interfaces;
using Claims.BusinessLogic.Services.Strategies;

namespace Claims.BusinessLogic.Services;

public class DefaultStrategyProvider : IDefaultStrategyProvider
{
    private readonly IDiscountProvider _discountProvider;

    public DefaultStrategyProvider(IDiscountProvider discountProvider)
    {
        _discountProvider = discountProvider;
    }

    public ICoverPremiumStrategy GetDefaultStrategy(CoverType type)
    {
        return new DefaultPremiumStrategy(type, _discountProvider);
    }
}
