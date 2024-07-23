using Microsoft.EntityFrameworkCore;
using Movilissa_api.Data.Context;
using Movilissa_api.Models;
using Movilissa.core.DTOs.Ticket;
using Movilissa.core.Interfaces.IServices;

namespace Movilissa.Infrastructure.Repositories;

public class ScheduleRepository : IScheduleRepository
{
    private readonly ApplicationDbContext _context;

    public ScheduleRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Schedule>> GetAvailableSchedules(TicketAvailabilityData data)
    {
        return await _context.Schedules
            .Include(s => s.Route)
            .ThenInclude(r => r.Destinations)
            .Include(s => s.Company)
            .Include(s => s.BusSchedules)
            .ThenInclude(bs => bs.Bus)
            .Where(s => s.Route.OriginId == data.OriginId &&
                        s.Route.Destinations.Any(d => d.DestinationId == data.DestinyId) &&
                        s.DepartureTime.Date == data.Date.Date &&
                        (!data.CompanyId.HasValue || s.CompanyId == data.CompanyId))
            .ToListAsync();
    }
}