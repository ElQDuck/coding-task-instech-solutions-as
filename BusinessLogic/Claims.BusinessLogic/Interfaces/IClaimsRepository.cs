using Claims.BusinessLogic.Entities;

namespace Claims.BusinessLogic.Interfaces
{
    public interface IClaimsRepository
    {
        Task<Result<IEnumerable<Claim>>> GetClaimsAsync();
        Task<Result<Claim>> GetClaimAsync(string id);
        Task<Result<Claim>>  AddClaimAsync(Claim item);
        Task<Result> DeleteClaimAsync(string id);
    }
}
