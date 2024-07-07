using Microsoft.EntityFrameworkCore;
using Movilissa_api.Data.Context;
using Movilissa_api.Data.IRepositories;
using Movilissa_api.Models;

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
}