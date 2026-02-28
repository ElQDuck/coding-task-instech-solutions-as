using Claims.BusinessLogic.Entities;
using Claims.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Claims.API.Controllers;

[ApiController]
public class CoversController : ControllerBase
{
    private readonly ICoversService _coversService;
    private readonly ILogger<CoversController> _logger;

    public CoversController(ICoversService coversService, ILogger<CoversController> logger)
    {
        _coversService = coversService;
        _logger = logger;
    }

    [HttpPost("Cover/compute")]
    public ActionResult ComputePremium(DateTime startDate, DateTime endDate, CoverType coverType)
    {
        return Ok(_coversService.ComputePremium(startDate, endDate, coverType));
    }

    [HttpGet("Covers")]
    public async Task<ActionResult<IEnumerable<Cover>>> GetAsync()
    {
        var results = await _coversService.GetCoversAsync();
        return Ok(results);
    }

    [HttpGet("Cover/{id}")]
    public async Task<ActionResult<Cover>> GetAsync(string id)
    {
        var cover = await _coversService.GetCoverAsync(id);
        return Ok(cover);
    }

    [HttpPost("Cover")]
    public async Task<ActionResult> CreateAsync(Cover cover)
    {
        var createdCover = await _coversService.CreateCoverAsync(cover);
        return Ok(createdCover);
    }

    [HttpDelete("Cover/{id}")]
    public async Task DeleteAsync(string id)
    {
        await _coversService.DeleteCoverAsync(id);
    }
}
