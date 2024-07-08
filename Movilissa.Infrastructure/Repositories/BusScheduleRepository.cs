using Microsoft.EntityFrameworkCore;
using Movilissa_api.Data.Context;
using Movilissa_api.Data.IRepositories;
using Movilissa_api.Models;

namespace Movilissa.Infrastructure.Repositories;

public class BusScheduleRepository : GenericRepository<BusSchedule>, IBusScheduleRepository
{
    private readonly ApplicationDbContext _context;

    public BusScheduleRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<BusSchedule>> GetAllBusSchedulesWithDetails()
    {
        return await _context.BusSchedules
            .Include(bs => bs.Bus)
            .ThenInclude(b => b.BusType)
            .Include(bs => bs.Schedule)
            .ThenInclude(s => s.Route)
            .ThenInclude(r => r.Origin)
            .Include(bs => bs.Schedule)
            .ThenInclude(s => s.Route)
            .ThenInclude(r => r.Destinations)
            .ThenInclude(rd => rd.Destination)
            .ToListAsync();
    }
}
