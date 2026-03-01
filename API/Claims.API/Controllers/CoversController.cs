using Claims.BusinessLogic.Entities;
using Claims.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Claims.API.Controllers;

/// <summary>
/// The REST API controller for covers.
/// </summary>
[ApiController]
public class CoversController : ControllerBase
{
    private readonly ICoversService _coversService;
    private readonly IPremiumComputeService _premiumComputeService;
    private readonly ILogger<CoversController> _logger;

    /// <summary>
    /// Initializes an instance of the <see cref="CoversController"/> class.
    /// </summary>
    /// <param name="coversService">The covers service.</param>
    /// <param name="premiumComputeService">The premium compute service.</param>
    /// <param name="logger">The logger.</param>
    public CoversController(ICoversService coversService, IPremiumComputeService premiumComputeService, ILogger<CoversController> logger)
    {
        _coversService = coversService;
        _premiumComputeService = premiumComputeService;
        _logger = logger;
    }

    /// <summary>
    /// The route to compute premium for a cover.
    /// </summary>
    /// <param name="startDate">The start date of the cover.</param>
    /// <param name="endDate">The end date of the cover.</param>
    /// <param name="coverType">The type of the cover.</param>
    /// <returns>The computed premium.</returns>
    [HttpPost("Cover/compute")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult ComputePremiumAsync(DateTime startDate, DateTime endDate, CoverType coverType)
    {
        return Ok(_premiumComputeService.ComputePremium(startDate, endDate, coverType));
    }

    /// <summary>
    /// The route to get all covers.
    /// </summary>
    /// <returns>A list of all existing covers.</returns>
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

    /// <summary>
    /// The route to get a cover by its ID.
    /// </summary>
    /// <param name="id">The cover ID.</param>
    /// <returns>The requested cover.</returns>
    [HttpGet("Cover/{id}")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Cover))]
    public async Task<ActionResult<Cover>> GetCoverAsync(string id)
    {
        var result = await _coversService.GetCoverAsync(id);
        result.EnsureSuccess();
        return Ok(result.Value);
    }

    /// <summary>
    /// The route to create a cover.
    /// </summary>
    /// <param name="cover">The cover to create.</param>
    /// <returns>The created cover.</returns>
    [HttpPost("Cover")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Cover))]
    public async Task<ActionResult> CreateCoverAsync(Cover cover)
    {
        // TODO: Hide ID from user because its set by BusinessLogic (QoL improvements)
        // TODO: why should the user set the premium if all information is provided to calculate it automatically (QoL improvements)
        var result = await _coversService.CreateCoverAsync(cover);
        result.EnsureSuccess();
        return Ok(result.Value);
    }

    /// <summary>
    /// The route to delete a cover.
    /// </summary>
    /// <param name="id">The ID of the cover to delete.</param>
    /// <returns>An HTTP 204 NoContent result.</returns>
    [HttpDelete("Cover/{id}")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult> DeleteCoverAsync(string id)
    {
        var result = await _coversService.DeleteCoverAsync(id);
        result.EnsureSuccess();
        return NoContent();
    }
}
