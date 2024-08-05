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
    public async Task<IEnumerable<BusList>> FilterBus(BusFilter filter)
    {
        return await _context.Buses
            .Include(b => b.BusType)
            .Include(b => b.Company)
            .Where(b => 
                (string.IsNullOrEmpty(filter.IdentificationNumber) || b.IdentificationNumber.Contains(filter.IdentificationNumber))
                && (string.IsNullOrEmpty(filter.LicensePlate) || b.LicensePlate.Contains(filter.LicensePlate))
                && (!filter.BrandId.HasValue || b.BusType.BrandId == filter.BrandId)
                && (!filter.StatusId.HasValue || b.StatusId == filter.StatusId)
            )
            .Select(b => new BusList
            {
                Id = b.Id,
                IdentificationNumber = b.IdentificationNumber,
                LicensePlate = b.LicensePlate,
                Model = b.BusType.Model,
                Brand = b.BusType.Brand.Name, // Asumiendo que Brand es un objeto dentro de BusType
                SeatingCapacity = b.BusType.SeatingCapacity,
                Status = new Item { Id = b.StatusId ?? default, Description = ((BusStatusEnum)(b.StatusId ?? default)).ToString() }
            })
            .ToListAsync();
    }
    public async Task<IEnumerable<Amenity>> FilterAmenities(AmenityFilter filter)
    {
        return await _context.Amenities
            .Where(a => (string.IsNullOrEmpty(filter.Name) || a.Name.Contains(filter.Name))
                        && (!filter.StatusId.HasValue || a.Status == filter.StatusId.Value))
            .ToListAsync();
    }
    public async Task<IEnumerable<BusTypeList>> FilterBusTypes(BusTypeFilter filter)
    {
        return await _context.BusTypes
            .Include(bt => bt.Brand)
            .Where(bt => 
                (string.IsNullOrEmpty(filter.Name) || bt.Model.Contains(filter.Name))
                && (!filter.StatusId.HasValue || bt.Status == filter.StatusId)
            )
            .Select(bt => new BusTypeList
            {
                Id = bt.Id,
                Brand = new Item { Id = bt.Brand.Id, Description = bt.Brand.Name },
                Model = bt.Model,
                Capacity = bt.SeatingCapacity,
                Status = new Item { Id = bt.Status, Description = ((GenericStatus)bt.Status).ToString() }
            })
            .ToListAsync();
    }
}