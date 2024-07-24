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

    public async Task<IEnumerable<Route>> GetAvailableRoutes(TicketAvailabilityData data)
    {
        Console.WriteLine($"OriginId: {data.OriginId}, DestinyId: {data.DestinyId}, Date: {data.Date}, CompanyId: {data.CompanyId}");

        // Consultar RouteDestinations para obtener los registros que coinciden con el DestinyId
        var matchingRouteDestinations = await _context.RouteDestinations
            .Include(rd => rd.Destination)
            .ThenInclude(d => d.Province)
            .Include(rd => rd.Route)
            .ThenInclude(r => r.Origin)
            .ThenInclude(o => o.Province)
            .Where(rd => rd.Destination.Province.Id == data.DestinyId)
            .ToListAsync();

        // Filtrar las rutas que tienen el OriginId especificado
        var matchingRoutes = matchingRouteDestinations
            .Where(rd => rd.Route.Origin.Province.Id == data.OriginId)
            .Select(rd => rd.Route)
            .Distinct()
            .ToList();

        return matchingRoutes;
    }


}
