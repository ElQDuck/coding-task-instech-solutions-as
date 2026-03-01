using Claims.BusinessLogic.Entities;
using Claims.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;

// 
namespace Claims.API.Controllers
{
    [ApiController]
    public class ClaimsController : ControllerBase
    {
        private readonly ILogger<ClaimsController> _logger;
        private readonly IClaimsService _claimsService;

        public ClaimsController(ILogger<ClaimsController> logger, IClaimsService claimsService)
        {
            _logger = logger;
            _claimsService = claimsService;
        }

        [HttpGet("Claims")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Claim>))]
        public async Task<ActionResult> GetClaimsAsync()
        {
            var result = await _claimsService.GetClaimsAsync();
            result.EnsureSuccess();
            return Ok(result.Value);
        }

        [HttpGet("Claim/{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Claim))]
        public async Task<ActionResult> GetClaimAsync(string id)
        {
            var result = await _claimsService.GetClaimAsync(id);
            result.EnsureSuccess();
            return Ok(result.Value);
        }

        [HttpPost("Claim")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Claim))]
        public async Task<ActionResult> CreateClaimAsync(Claim claim)
        {
            var result = await _claimsService.CreateClaimAsync(claim);
            result.EnsureSuccess();
            return Ok(result.Value);
        }

        [HttpDelete("Claim/{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> DeleteClaimAsync([FromRoute]string id)
        {
            var result = await _claimsService.DeleteClaimAsync(id);
            result.EnsureSuccess();
            return Ok();
        }
    }
}
