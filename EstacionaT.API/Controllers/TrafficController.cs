using EstacionaT.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace EstacionaT.API.Controllers;

[ApiController]
[Route("[controller]")]
public class TrafficController : ControllerBase
{
    private readonly ITrafficService _trafficService;

    public TrafficController(ITrafficService trafficService)
    {
        _trafficService = trafficService;
    }

    [HttpPost("activate")]
    public async Task<IActionResult> ActivateCongestionMode(int extraToleranceMinutes = 0, bool unlimitedMode = false)
    {
        await _trafficService.ActivateCongestionModeAsync(extraToleranceMinutes, unlimitedMode);
        return Ok("Congestion mode activated.");
    }

    [HttpPost("deactivate")]
    public async Task<IActionResult> DeactivateCongestionMode()
    {
        await _trafficService.DeactivateCongestionModeAsync();
        return Ok("Congestion mode deactivated.");
    }
}