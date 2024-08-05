using Microsoft.EntityFrameworkCore;
using Movilissa_api.Data.Context;
using Movilissa_api.Data.IRepositories;
using Movilissa_api.Models;
using Movilissa.core.DTOs.Bus.AmenityDTOs;

namespace Movilissa.Infrastructure.Repositories;

public class BusRepository : GenericRepository<Bus>, IBusRepository
{
    private readonly ApplicationDbContext _context;

    public BusRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<Bus> GetBusWithDetails(int busId)
    {
        return await _context.Buses
            .Include(b => b.Company)
            .Include(b => b.BusType)
            .Include(b => b.Amenities)
                .ThenInclude(a => a.Amenity)
            .FirstOrDefaultAsync(b => b.Id == busId);
    }
    public async Task<IEnumerable<Amenity>> FilterAmenities(AmenityFilter filter)
    {
        return await _context.Amenities
            .Where(a => (string.IsNullOrEmpty(filter.Name) || a.Name.Contains(filter.Name))
                        && (!filter.StatusId.HasValue || a.Status == filter.StatusId.Value))
            .ToListAsync();
    }
}