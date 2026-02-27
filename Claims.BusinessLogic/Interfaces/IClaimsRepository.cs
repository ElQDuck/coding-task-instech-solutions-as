using Claims.BusinessLogic.Entities;

namespace Claims.BusinessLogic.Interfaces
{
    public interface IClaimsRepository
    {
        Task<IEnumerable<Claim>> GetClaimsAsync();
        Task<Claim> GetClaimAsync(string id);
        Task AddClaimAsync(Claim item);
        Task DeleteClaimAsync(string id);
    }
}
