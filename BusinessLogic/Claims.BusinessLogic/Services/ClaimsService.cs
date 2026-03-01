using Claims.BusinessLogic.Entities;
using Claims.BusinessLogic.Interfaces;

namespace Claims.BusinessLogic.Services
{
    /// <summary>
    /// Implements the service for managing claims.
    /// </summary>
    public class ClaimsService : IClaimsService
    {
        private readonly IClaimsRepository _claimsRepository;
        private readonly ICoversService _coversService;

        /// <summary>
        /// Initializes an instance of the <see cref="ClaimsService"/> class.
        /// </summary>
        /// <param name="claimsRepository">The claims repository.</param>
        /// <param name="coversService">The covers service.</param>
        public ClaimsService(IClaimsRepository claimsRepository, ICoversService coversService)
        {
            _claimsRepository = claimsRepository;
            _coversService = coversService;
        }

        /// <inheritdoc/>
        public async Task<Result<IEnumerable<Claim>>> GetClaimsAsync()
        {
            return await _claimsRepository.GetClaimsAsync();
        }

        /// <inheritdoc/>
        public async Task<Result<Claim>> GetClaimAsync(string id)
        {
            return await _claimsRepository.GetClaimAsync(id);
        }

        /// <inheritdoc/>
        public async Task<Result<Claim>> CreateClaimAsync(Claim claim)
        {
            // Created date must be within the period of the related Cover 
            var coverResult = await _coversService.GetCoverAsync(claim.CoverId);
            coverResult.EnsureSuccess();

            var cover = coverResult.Value;
            if (claim.Created < cover.StartDate || claim.Created > cover.EndDate)
            {
                var error = new ArgumentException(Resources.ErrorMessages.E_ClaimDateOutsideCoverPeriod);
                return Result.FromException<Claim>(error);
            }

            // DamageCost cannot exceed 100.000
            const int maxDamageCost = 100000;
            if (claim.DamageCost > maxDamageCost)
            {
                var error = new ArgumentException(string.Format(Resources.ErrorMessages.E_DamageCostHigherThan, maxDamageCost));
                return Result.FromException<Claim>(error);
            }
            claim.Id = Guid.NewGuid().ToString();
            await _claimsRepository.AddClaimAsync(claim);
            return Result.FromSuccess(claim);
        }

        /// <inheritdoc/>
        public async Task<Result> DeleteClaimAsync(string id)
        {
            return await _claimsRepository.DeleteClaimAsync(id);
        }
    }
}
