using Claims.BusinessLogic.Entities;
using Claims.BusinessLogic.Interfaces;

namespace Claims.BusinessLogic.Services
{
    public class CoversService : ICoversService
    {
        private readonly ICoversRepository _repository;
        private readonly IPremiumCalculationService _premiumCalculationService;

        public CoversService(ICoversRepository repository, IPremiumCalculationService premiumCalculationService)
        {
            _repository = repository;
            _premiumCalculationService = premiumCalculationService;
        }

        public async Task<Result<IEnumerable<Cover>>> GetCoversAsync()
        {
            return await _repository.GetCoversAsync();
        }

        public async Task<Result<Cover>> GetCoverAsync(string id)
        {
            return await _repository.GetCoverAsync(id);
        }

        public async Task<Result<Cover>> CreateCoverAsync(Cover cover)
        {
            cover.Id = Guid.NewGuid().ToString();
            cover.Premium = _premiumCalculationService.CalculatePremium(cover.StartDate, cover.EndDate, cover.Type);
            await _repository.AddCoverAsync(cover);
            return cover;
        }

        public async Task<Result> DeleteCoverAsync(string id)
        {
            return await _repository.DeleteCoverAsync(id);
        }
    }
}
