using Microsoft.AspNetCore.Mvc;
using Movilissa_api.Logic;
using Movilissa_api.Models;

namespace Movilissa_api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SearchController : ControllerBase
{
    private readonly SearchLogic _searchLogic;

    public SearchController(SearchLogic searchLogic)
    {
        _searchLogic = searchLogic;
    }
    [HttpGet("search")]
    public async Task<ActionResult<IEnumerable<Schedule>>> SearchSchedules(int origin, int destination, DateTime? date, int company)
    {
        var schedules = await _searchLogic.SearchSchedulesAsync(origin, destination, date, company);
        return Ok(schedules);
    }

}
