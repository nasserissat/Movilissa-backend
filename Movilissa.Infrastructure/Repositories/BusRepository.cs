using Microsoft.EntityFrameworkCore;
using Movilissa_api.Data.Context;
using Movilissa_api.Data.IRepositories;
using Movilissa_api.Enums;
using Movilissa_api.Models;
using Movilissa.core.DTOs.Bus;
using Movilissa.core.DTOs.Bus.AmenityDTOs;
using Movilissa.core.DTOs.Shared;

namespace Movilissa.Infrastructure.Repositories;

public class BusRepository : GenericRepository<Bus>, IBusRepository
{
    private readonly ApplicationDbContext _context;

    public BusRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<List<BusList>> GetBusList()
    {
        return await _context.Buses
            .Include(b => b.Company)
            .Include(b => b.BusType)
            .ThenInclude(bt => bt.Brand)
            .Include(b => b.Amenities)
            .ThenInclude(a => a.Amenity)
            .Select(b => new BusList
            {
                Id = b.Id,
                IdentificationNumber = b.IdentificationNumber,
                LicensePlate = b.LicensePlate,
                Brand = b.BusType.Brand.Name,
                Model = b.BusType.Model,
                SeatingCapacity = b.BusType.SeatingCapacity,
                Status = Item.From((BusStatusEnum)b.StatusId)
            }).ToListAsync();
    }
    public async Task<Bus> GetBusWithDetails(int busId)
    {
        return await _context.Buses
            .Include(b => b.Company)
            .Include(b => b.BusType)
                .ThenInclude(bt => bt.Brand)
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
    public async Task<IEnumerable<BusType>> FilterBusTypes(BusTypeFilter filter)
    {
        return await _context.BusTypes
            .Where(bt => (string.IsNullOrEmpty(filter.Name) || bt .Model.Contains(filter.Name))
                        && (!filter.StatusId.HasValue || bt .Status == filter.StatusId.Value))
            .ToListAsync();
    }
}