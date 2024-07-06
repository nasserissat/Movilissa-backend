using Microsoft.EntityFrameworkCore;
using Movilissa_api.Data.Context;
using Movilissa_api.Data.IRepositories;
using Movilissa_api.Models;

namespace Movilissa_api.Data.Repositories;

public class SearchRepository  : ISearchRepository
{
    private readonly ApplicationDbContext _context;
    public SearchRepository (ApplicationDbContext context)
    {
        _context = context;
    }

//     public async Task<List<Schedule>> SearchSchedulesAsync(int origin, int destination, DateTime? date, int company) =>
//        await _context.Schedules
//             .Include(s => s.Route)
//             .ThenInclude(r => r.Origin)
//             .Include(s => s.Route)
//             .ThenInclude(r => r.Destination)
//             .Where(s => s.Route.OriginId == origin)
//             .Where(s => s.Route.DestinationId == destination)
//             .Where(s => company == null || s.CompanyId == company)
//             // .Where(s => s.date == date)
//             .ToListAsync();
}