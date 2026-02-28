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
            // StartDate cannot be in the past
            if (cover.StartDate < DateTime.UtcNow)
            {
                var error = new ArgumentException("Start date cannot be in the past.");
                return Result.FromException<Cover>(error);
            }
            
            // Total insurance period cannot exceed 1 year.
            // Making sure that we don't get problems with hours, minutes, seconds...
            var startTime = cover.StartDate.Date;
            var endTime = cover.EndDate.Date;
            if (endTime > startTime.AddYears(1))
            {
                var error = new ArgumentException("Total insurance period cannot exceed 1 year.");
                return Result.FromException<Cover>(error);
            }
            
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
