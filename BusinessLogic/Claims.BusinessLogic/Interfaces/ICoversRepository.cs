using Claims.BusinessLogic.Entities;

namespace Claims.BusinessLogic.Interfaces
{
    public interface ICoversRepository
    {
        Task<IEnumerable<Cover>> GetCoversAsync();
        Task<Cover> GetCoverAsync(string id);
        Task AddCoverAsync(Cover item);
        Task DeleteCoverAsync(string id);
    }
}
