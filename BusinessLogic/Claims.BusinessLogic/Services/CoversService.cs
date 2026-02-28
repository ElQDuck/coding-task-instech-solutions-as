using Claims.BusinessLogic.Entities;
using Claims.BusinessLogic.Interfaces;

namespace Claims.BusinessLogic.Services
{
    public class CoversService : ICoversService
    {
        private readonly ICoversRepository _repository;
        private readonly Auditer _auditer;

        public CoversService(ICoversRepository repository, Auditer auditer)
        {
            _repository = repository;
            _auditer = auditer;
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
            cover.Premium = ComputePremium(cover.StartDate, cover.EndDate, cover.Type);
            await _repository.AddCoverAsync(cover);
            // TODO: Check if this can be done in businessLogic
            _auditer.AuditCover(cover.Id, "POST");
            return cover;
        }

        public async Task<Result> DeleteCoverAsync(string id)
        {
            // TODO: Why is this here?
            //_auditer.AuditCover(id, "DELETE");
            return await _repository.DeleteCoverAsync(id);
        }

        public decimal ComputePremium(DateTime startDate, DateTime endDate, CoverType coverType)
        {
            var multiplier = 1.3m;
            if (coverType == CoverType.Yacht)
            {
                multiplier = 1.1m;
            }

            if (coverType == CoverType.PassengerShip)
            {
                multiplier = 1.2m;
            }

            if (coverType == CoverType.Tanker)
            {
                multiplier = 1.5m;
            }

            var premiumPerDay = 1250 * multiplier;
            var insuranceLength = (endDate - startDate).TotalDays;
            var totalPremium = 0m;

            for (var i = 0; i < insuranceLength; i++)
            {
                if (i < 30) totalPremium += premiumPerDay;
                if (i < 180 && coverType == CoverType.Yacht) totalPremium += premiumPerDay - premiumPerDay * 0.05m;
                else if (i < 180) totalPremium += premiumPerDay - premiumPerDay * 0.02m;
                if (i < 365 && coverType != CoverType.Yacht) totalPremium += premiumPerDay - premiumPerDay * 0.03m;
                else if (i < 365) totalPremium += premiumPerDay - premiumPerDay * 0.08m;
            }

            return totalPremium;
        }
    }
}
