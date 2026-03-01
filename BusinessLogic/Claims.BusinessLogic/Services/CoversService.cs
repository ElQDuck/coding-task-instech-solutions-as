using Claims.BusinessLogic.Entities;
using Claims.BusinessLogic.Interfaces;

namespace Claims.BusinessLogic.Services
{
    /// <summary>
    /// Implements the service for managing covers.
    /// </summary>
    public class CoversService : ICoversService
    {
        private readonly ICoversRepository _repository;
        private readonly IPremiumComputeService _premiumComputeService;

        /// <summary>
        /// Initializes an instance of the <see cref="CoversService"/> class.
        /// </summary>
        /// <param name="repository">The covers repository.</param>
        /// <param name="premiumComputeService">The premium compute service.</param>
        public CoversService(ICoversRepository repository, IPremiumComputeService premiumComputeService)
        {
            _repository = repository;
            _premiumComputeService = premiumComputeService;
        }

        /// <inheritdoc/>
        public async Task<Result<IEnumerable<Cover>>> GetCoversAsync()
        {
            return await _repository.GetCoversAsync();
        }

        /// <inheritdoc/>
        public async Task<Result<Cover>> GetCoverAsync(string id)
        {
            return await _repository.GetCoverAsync(id);
        }

        /// <inheritdoc/>
        public async Task<Result<Cover>> CreateCoverAsync(Cover cover)
        {
            // StartDate cannot be in the past
            if (cover.StartDate < DateTime.UtcNow)
            {
                var error = new ArgumentException(Resources.ErrorMessages.E_StartDateInThePast);
                return Result.FromException<Cover>(error);
            }
            
            // Total insurance period cannot exceed 1 year.
            // Making sure that we don't get problems with hours, minutes, seconds...
            var startTime = cover.StartDate.Date;
            var endTime = cover.EndDate.Date;
            if (endTime > startTime.AddYears(1))
            {
                var error = new ArgumentException(Resources.ErrorMessages.E_EnsurancePeriorExeedsOneYear);
                return Result.FromException<Cover>(error);
            }
            
            // Make sure that EndDate is after StartDate (possible input error)
            if (cover.StartDate > cover.EndDate)
            {
                var error = new ArgumentException(Resources.ErrorMessages.E_StartDateAfterEndDate);
                return Result.FromException<Cover>(error);
            }
            
            cover.Id = Guid.NewGuid().ToString();
            cover.Premium = _premiumComputeService.ComputePremium(cover.StartDate, cover.EndDate, cover.Type);
            await _repository.AddCoverAsync(cover);
            return cover;
        }

        /// <inheritdoc/>
        public async Task<Result> DeleteCoverAsync(string id)
        {
            return await _repository.DeleteCoverAsync(id);
        }
    }
}
