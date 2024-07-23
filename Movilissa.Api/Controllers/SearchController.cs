using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Movilissa.core.DTOs.Ticket;
using Movilissa.core.Interfaces.IServices;

namespace Movilissa_api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SearchController : ControllerBase
{
    private readonly IScheduleService _scheduleService;

    public SearchController(IScheduleService scheduleService)
    {
        _scheduleService = scheduleService;
    }

    [AllowAnonymous]
    [HttpGet("tickets_availables")]
    public async Task<IActionResult> CheckTicketAvailability([FromQuery] TicketAvailabilityData data)
    {
        var response = await _scheduleService.CheckTicketAvailability(data);

        if (response.IsSuccess)
            return Ok(response.Data);

        return BadRequest(new { message = response.Message, errors = response.Errors });
    }
}