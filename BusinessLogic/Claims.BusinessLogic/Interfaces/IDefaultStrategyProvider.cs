using Claims.BusinessLogic.Entities;

namespace Claims.BusinessLogic.Interfaces;

public interface IDefaultStrategyProvider
{
    ICoverPremiumStrategy GetDefaultStrategy(CoverType type);
}
