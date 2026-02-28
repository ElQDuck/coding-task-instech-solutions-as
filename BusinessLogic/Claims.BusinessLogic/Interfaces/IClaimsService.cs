using Claims.BusinessLogic.Entities;

namespace Claims.BusinessLogic.Interfaces
{
    public interface IClaimsService
    {
        Task<Result<IEnumerable<Claim>>> GetClaimsAsync();
        Task<Result<Claim>> GetClaimAsync(string id);
        Task<Result<Claim>> CreateClaimAsync(Claim claim);
        Task<Result> DeleteClaimAsync(string id);
    }
}
