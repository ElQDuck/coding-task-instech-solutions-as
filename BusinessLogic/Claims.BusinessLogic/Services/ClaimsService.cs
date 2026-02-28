using Claims.BusinessLogic.Entities;
using Claims.BusinessLogic.Interfaces;

namespace Claims.BusinessLogic.Services
{
    public class ClaimsService : IClaimsService
    {
        private readonly IClaimsRepository _repository;
        private readonly Auditer _auditer;

        public ClaimsService(IClaimsRepository repository, Auditer auditer)
        {
            _repository = repository;
            _auditer = auditer;
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
            _auditer.AuditClaim(claim.Id, "POST");
            return claim;
        }

        public async Task<Result> DeleteClaimAsync(string id)
        {
            return await _repository.DeleteClaimAsync(id);
        }
    }
}
