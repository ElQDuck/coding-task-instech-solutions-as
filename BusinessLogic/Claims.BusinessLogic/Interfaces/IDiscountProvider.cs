using Claims.BusinessLogic.Entities;

namespace Claims.BusinessLogic.Interfaces;

public interface IDiscountProvider
{
    decimal GetDiscountForDay(int dayIndex);
}
