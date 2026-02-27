using Claims.BusinessLogic.Entities;

namespace Claims.BusinessLogic.Interfaces
{
    public interface ICoversService
    {
        decimal ComputePremium(DateTime startDate, DateTime endDate, CoverType coverType);
        Task<IEnumerable<Cover>> GetCoversAsync();
        Task<Cover> GetCoverAsync(string id);
        Task<Cover> CreateCoverAsync(Cover cover);
        Task DeleteCoverAsync(string id);
    }
}
