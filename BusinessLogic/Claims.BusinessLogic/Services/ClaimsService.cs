using Claims.BusinessLogic.Entities;
using Claims.BusinessLogic.Interfaces;

namespace Claims.BusinessLogic.Services
{
    public class ClaimsService : IClaimsService
    {
        private readonly IClaimsRepository _claimsRepository;

        public ClaimsService(IClaimsRepository claimsRepository)
        {
            _claimsRepository = claimsRepository;
        }

        public async Task<Result<IEnumerable<Claim>>> GetClaimsAsync()
        {
            return await _claimsRepository.GetClaimsAsync();
        }

        public async Task<Result<Claim>> GetClaimAsync(string id)
        {
            return await _claimsRepository.GetClaimAsync(id);
        }

        public async Task<Result<Claim>> CreateClaimAsync(Claim claim)
        {
            // TODO: Check for existing coverId?
            const int maxDamageCost = 100000;
            if (claim.DamageCost > maxDamageCost)
            {
                var error = new ArgumentException($"Damage cost must be less than {maxDamageCost}.");
                return Result.FromException<Claim>(error);
            }
            claim.Id = Guid.NewGuid().ToString();
            await _claimsRepository.AddClaimAsync(claim);
            return Result.FromSuccess(claim);
        }

        public async Task<Result> DeleteClaimAsync(string id)
        {
            return await _claimsRepository.DeleteClaimAsync(id);
        }
    }
}
