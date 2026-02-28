using Claims.BusinessLogic.Entities;

namespace Claims.BusinessLogic.Interfaces;

public interface IPremiumCalculationService
{
    decimal CalculatePremium(DateTime startDate, DateTime endDate, CoverType coverType);
}