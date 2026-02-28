using Claims.BusinessLogic.Entities;

namespace Claims.BusinessLogic.Interfaces;

public interface IDiscountProvider
{
    IEnumerable<decimal> GetDiscounts(CoverType type, int dayIndex);
}
