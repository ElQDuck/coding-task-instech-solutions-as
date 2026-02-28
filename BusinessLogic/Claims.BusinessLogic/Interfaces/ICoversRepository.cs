using Claims.BusinessLogic.Entities;

namespace Claims.BusinessLogic.Interfaces
{
    public interface ICoversRepository
    {
        Task<Result<IEnumerable<Cover>>> GetCoversAsync();
        Task<Result<Cover>> GetCoverAsync(string id);
        Task<Result<Cover>> AddCoverAsync(Cover item);
        Task<Result> DeleteCoverAsync(string id);
    }
}
