using Claims.BusinessLogic.Entities;

namespace Claims.BusinessLogic.Interfaces
{
    public interface ICoversService
    {
        decimal ComputePremium(DateTime startDate, DateTime endDate, CoverType coverType);
        Task<Result<IEnumerable<Cover>>> GetCoversAsync();
        Task<Result<Cover>> GetCoverAsync(string id);
        Task<Result<Cover>> CreateCoverAsync(Cover cover);
        Task<Result> DeleteCoverAsync(string id);
    }
}
