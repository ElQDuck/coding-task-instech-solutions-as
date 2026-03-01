using Claims.BusinessLogic.Entities;

namespace Claims.BusinessLogic.Interfaces;

public interface IPremiumComputeService
{
    decimal ComputePremium(DateTime startDate, DateTime endDate, CoverType coverType);
}