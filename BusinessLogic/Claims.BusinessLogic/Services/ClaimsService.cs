using Claims.BusinessLogic.Entities;
using Claims.BusinessLogic.Interfaces;

namespace Claims.BusinessLogic.Services
{
    public class ClaimsService : IClaimsService
    {
        private readonly IClaimsRepository _claimsRepository;
        private readonly ICoversService _coversService;

        public ClaimsService(IClaimsRepository claimsRepository, ICoversService coversService)
        {
            _claimsRepository = claimsRepository;
            _coversService = coversService;
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
            // Created date must be within the period of the related Cover 
            var coverResult = await _coversService.GetCoverAsync(claim.CoverId);
            coverResult.EnsureSuccess();

            var cover = coverResult.Value;
            if (claim.Created < cover.StartDate || claim.Created > cover.EndDate)
            {
                var error = new ArgumentException("Claim date must be within the cover period.");
                return Result.FromException<Claim>(error);
            }

            // DamageCost cannot exceed 100.000
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
