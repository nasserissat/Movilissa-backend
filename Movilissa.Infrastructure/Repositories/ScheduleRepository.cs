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
        
            Console.WriteLine(
                $"OriginId: {data.OriginId}, DestinyId: {data.DestinyId}, Date: {data.Date}, CompanyId: {data.CompanyId}");

            var schedulesQuery = _context.Schedules
                .Include(s => s.Route)
                .ThenInclude(r => r.Origin)
                .ThenInclude(b => b.Province)
                .Include(s => s.Route)
                .ThenInclude(r => r.Destinations)
                .ThenInclude(rd => rd.Destination)
                .ThenInclude(d => d.Province)
                .Include(s => s.Company)
                .Include(s => s.BusSchedules)
                .ThenInclude(bs => bs.Bus)
                .Where(s => s.Route.Origin.Province.Id == data.OriginId &&
                            s.Route.Destinations.Any(rd => rd.Destination.Province.Id == data.DestinyId) &&
                            s.DepartureTime.Date == data.Date.Date &&
                            (!data.CompanyId.HasValue || s.CompanyId == data.CompanyId));

            Console.WriteLine($"Query: {schedulesQuery.ToQueryString()}");

            var schedules = await schedulesQuery.ToListAsync();
            return schedules;

    }

}
