using Claims.BusinessLogic.Entities;
using Claims.BusinessLogic.Interfaces;

namespace Claims.BusinessLogic.Services
{
    public class ClaimsService : IClaimsService
    {
        private readonly IClaimsRepository _repository;

        public ClaimsService(IClaimsRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<IEnumerable<Claim>>> GetClaimsAsync()
        {
            return await _repository.GetClaimsAsync();
        }

        public async Task<Result<Claim>> GetClaimAsync(string id)
        {
            return await _repository.GetClaimAsync(id);
        }

        public async Task<Result<Claim>> CreateClaimAsync(Claim claim)
        {
            claim.Id = Guid.NewGuid().ToString();
            await _repository.AddClaimAsync(claim);
            return claim;
        }

        public async Task<Result> DeleteClaimAsync(string id)
        {
            return await _repository.DeleteClaimAsync(id);
        }
    }
}
