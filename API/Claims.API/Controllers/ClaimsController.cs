using Claims.BusinessLogic.Entities;
using Claims.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Claims.API.Controllers
{
    /// <summary>
    /// The REST API controller for claims.
    /// </summary>
    [ApiController]
    public class ClaimsController : ControllerBase
    {
        private readonly ILogger<ClaimsController> _logger;
        private readonly IClaimsService _claimsService;

        /// <summary>
        /// Initializes an instance of the <see cref="ClaimsController"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="claimsService">The claims service.</param>
        public ClaimsController(ILogger<ClaimsController> logger, IClaimsService claimsService)
        {
            _logger = logger;
            _claimsService = claimsService;
        }

        /// <summary>
        /// The route to get all claims.
        /// </summary>
        /// <returns>A list of all existing claims.</returns>
        [HttpGet("Claims")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Claim>))]
        public async Task<ActionResult> GetClaimsAsync()
        {
            var result = await _claimsService.GetClaimsAsync();
            result.EnsureSuccess();
            return Ok(result.Value);
        }

        /// <summary>
        /// The route to get a claim by its ID.
        /// </summary>
        /// <param name="id">The claim ID.</param>
        /// <returns>The requested claim.</returns>
        [HttpGet("Claim/{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Claim))]
        public async Task<ActionResult> GetClaimAsync(string id)
        {
            var result = await _claimsService.GetClaimAsync(id);
            result.EnsureSuccess();
            return Ok(result.Value);
        }

        /// <summary>
        /// The route to create a claim.
        /// </summary>
        /// <param name="claim">The claim to create.</param>
        /// <returns>The created claim.</returns>
        [HttpPost("Claim")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Claim))]
        public async Task<ActionResult> CreateClaimAsync(Claim claim)
        {
            var result = await _claimsService.CreateClaimAsync(claim);
            result.EnsureSuccess();
            return Ok(result.Value);
        }

        /// <summary>
        /// The route to delete a claim.
        /// </summary>
        /// <param name="id">The ID of the claim to delete.</param>
        /// <returns>An HTTP 204 NoContent result.</returns>
        [HttpDelete("Claim/{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> DeleteClaimAsync([FromRoute]string id)
        {
            var result = await _claimsService.DeleteClaimAsync(id);
            result.EnsureSuccess();
            return NoContent();
        }
    }
}
