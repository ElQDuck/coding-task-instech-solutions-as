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

        public async Task<IEnumerable<Claim>> GetClaimsAsync()
        {
            return await _repository.GetClaimsAsync();
        }

        public async Task<Claim> GetClaimAsync(string id)
        {
            return await _repository.GetClaimAsync(id);
        }

        public async Task<Claim> CreateClaimAsync(Claim claim)
        {
            claim.Id = Guid.NewGuid().ToString();
            await _repository.AddItemAsync(claim);
            _auditer.AuditClaim(claim.Id, "POST");
            return claim;
        }

        public async Task DeleteClaimAsync(string id)
        {
            _auditer.AuditClaim(id, "DELETE");
            await _repository.DeleteItemAsync(id);
        }
    }
}
