using EstacionaT.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace EstacionaT.API.Controllers;

[ApiController]
[Route("[controller]")]
public class TicketsController : ControllerBase
{
    private readonly ITicketService _ticketService;

    public TicketsController(ITicketService ticketService)
    {
        _ticketService = ticketService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateTicket()
    {
        var ticket = await _ticketService.CreateTicketAsync();
        return Ok(ticket);
    }

    [HttpPost("{id}/pay")]
    public async Task<IActionResult> PayTicket(int id)
    {
        var result = await _ticketService.PayTicketAsync(id);
        if (result)
        {
            return Ok();
        }
        return NotFound();
    }

    [HttpPost("{id}/validate")]
    public async Task<IActionResult> ValidateExit(int id)
    {
        var result = await _ticketService.ValidateExitAsync(id);
        if (result)
        {
            return Ok("Exit allowed.");
        }
        return Forbid("Exit not allowed.");
    }
}