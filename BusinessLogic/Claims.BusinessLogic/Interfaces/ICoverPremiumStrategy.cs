using Claims.BusinessLogic.Entities;

namespace Claims.BusinessLogic.Interfaces;

public interface ICoverPremiumStrategy
{
    CoverType SupportedType { get; }
    decimal CalculatePremium(DateTime startDate, DateTime endDate);
}
