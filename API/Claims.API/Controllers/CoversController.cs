using Claims.BusinessLogic.Entities;
using Claims.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Claims.API.Controllers;

[ApiController]
public class CoversController : ControllerBase
{
    private readonly ICoversService _coversService;
    private readonly IPremiumCalculationService _premiumCalculationService;
    private readonly ILogger<CoversController> _logger;

    public CoversController(ICoversService coversService, IPremiumCalculationService premiumCalculationService, ILogger<CoversController> logger)
    {
        _coversService = coversService;
        _premiumCalculationService = premiumCalculationService;
        _logger = logger;
    }

    [HttpPost("Cover/compute")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult ComputePremiumAsync(DateTime startDate, DateTime endDate, CoverType coverType)
    {
        return Ok(_premiumCalculationService.CalculatePremium(startDate, endDate, coverType));
    }

    [HttpGet("Covers")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Cover>))]
    public async Task<ActionResult<IEnumerable<Cover>>> GetCoversAsync()
    {
        var result = await _coversService.GetCoversAsync();
        // TODO: Better error print out. To much information for user. Security concerns.
        result.EnsureSuccess();
        return Ok(result.Value);
    }

    [HttpGet("Cover/{id}")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Cover))]
    public async Task<ActionResult<Cover>> GetCoverAsync(string id)
    {
        var result = await _coversService.GetCoverAsync(id);
        result.EnsureSuccess();
        return Ok(result.Value);
    }

    [HttpPost("Cover")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Cover))]
    public async Task<ActionResult> CreateCoverAsync(Cover cover)
    {
        var result = await _coversService.CreateCoverAsync(cover);
        result.EnsureSuccess();
        return Ok(result.Value);
    }

    [HttpDelete("Cover/{id}")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> DeleteCoverAsync(string id)
    {
        var result = await _coversService.DeleteCoverAsync(id);
        result.EnsureSuccess();
        return Ok();
    }
}
