using System.Runtime.InteropServices.JavaScript;
using Claims.BusinessLogic.Entities;
using Claims.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Claims.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClaimsController : ControllerBase
    {
        private readonly ILogger<ClaimsController> _logger;
        private readonly IClaimsService _claimsService;

        public ClaimsController(ILogger<ClaimsController> logger, IClaimsService claimsService)
        {
            _logger = logger;
            _claimsService = claimsService;
        }

        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Claim>))]
        public async Task<ActionResult> GetClaimsAsync()
        {
            var result = await _claimsService.GetClaimsAsync();
            result.EnsureSuccess();
            return Ok(result.Value);
        }

        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Claim))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetClaimAsync(string id)
        {
            var result = await _claimsService.GetClaimAsync(id);
            result.EnsureSuccess();
            return Ok(result.Value);
        }

        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Claim))]
        public async Task<ActionResult> CreateClaimAsync(Claim claim)
        {
            var result = await _claimsService.CreateClaimAsync(claim);
            result.EnsureSuccess();
            return Ok(result.Value);
        }

        [HttpDelete("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteClaimAsync([FromRoute]string id)
        {
            var result = await _claimsService.DeleteClaimAsync(id);
            result.EnsureSuccess();
            return Ok();
        }
    }
}
