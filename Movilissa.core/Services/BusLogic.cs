using Movilissa_api.Data.IRepositories;
using Movilissa_api.Enums;
using Movilissa_api.Models;
using Movilissa.core.DTOs.Bus;
using Movilissa.core.DTOs.Shared;

namespace Movilissa_api.Logic;

public class BusLogic
{
    private readonly IBusRepository _busRepository;
    private readonly IBusScheduleRepository _busScheduleRepository;

    public BusLogic(IBusRepository busRepository, IBusScheduleRepository busScheduleRepository)
    {
        _busRepository = busRepository;
        _busScheduleRepository = busScheduleRepository;

    }

    #region Bus
    
    // Obtener todos los buses
    public async Task<IEnumerable<BusList>> GetAllBusesAsync()
    {
        var buses = await _busRepository.GetAllAsync(null, b => b.Company, b => b.BusType);
        return buses.Select(b => new BusList
        {
            Id = b.Id,
            IdentificationNumber = b.IdentificationNumber,
            LicensePlate = b.LicensePlate,
            Brand = b.BusType.Brand,
            Model = b.BusType.Model,
            SeatingCapacity = b.BusType.SeatingCapacity,
            Status = Item.From((BusStatusEnum)b.StatusId)
        }).ToList();
    }
    
    // Crear un nuevo Bus
    public async Task<int> CreateBusAsync(BusData busDTO)
    {
        if (busDTO == null)
            throw new ArgumentNullException(nameof(busDTO));

        // Realizar validaciones
        if (string.IsNullOrWhiteSpace(busDTO.IdentificationNumber) || string.IsNullOrWhiteSpace(busDTO.LicensePlate))
            throw new ArgumentException("El número de identificación y la matrícula son obligatorios.");

        var bus = new Bus
        {
            IdentificationNumber = busDTO.IdentificationNumber.Trim(),
            LicensePlate = busDTO.LicensePlate.Trim(),
            CompanyId = busDTO.CompanyId,
            BusTypeId = busDTO.BusTypeId,
            Amenities = new List<BusAmenity>()

        };
        
        foreach (var amenityId in busDTO.AmenityIds)
        {
            var amenity = new BusAmenity { AmenityId = amenityId, Bus = bus };
            bus.Amenities.Add(amenity);
        }

        await _busRepository.AddAsync(bus);
        return bus.Id;
    }

    // Obtener el detalle de un Bus
    public async Task<BusDetailed> GetBusDetailed(int id)
    {
        var bus = await _busRepository.GetBusWithDetails(id);
        if (bus == null)
            throw new Exception("Autobús no encontrado");

        return new BusDetailed
        {
            Id = bus.Id,
            IdentificationNumber = bus.IdentificationNumber,
            LicensePlate = bus.LicensePlate,
            CompanyName = bus.Company.Name,
            Model = bus.BusType.Model,
            Brand = bus.BusType.Brand,
            SeatingCapacity = bus.BusType.SeatingCapacity,
            Status = Item.From((BusStatusEnum)bus.StatusId),
            Amenities = bus.Amenities.Select(a => new Item { Id = a.Amenity.Id, Description = a.Amenity.Name }).ToList(),
        };
    }
    
    public async Task UpdateBusAsync(int id, BusData busData)
    {
        var bus = await _busRepository.GetByIdAsync(id);
        if (bus == null)
            throw new Exception("Autobús no encontrado");

        bus.IdentificationNumber = busData.IdentificationNumber.Trim();
        bus.LicensePlate = busData.LicensePlate.Trim();
        bus.CompanyId = busData.CompanyId;
        bus.BusTypeId = busData.BusTypeId;

        bus.Amenities.Clear();
        foreach (var amenityId in busData.AmenityIds)
        {
            bus.Amenities.Add(new BusAmenity { AmenityId = amenityId, BusId = bus.Id });
        }

        await _busRepository.Update(bus);
    }

    // Eliminar un Autobus
    public async Task DeleteBusAsync(int id)
    {
        var bus = await _busRepository.GetByIdAsync(id);
        if (bus == null)
            throw new Exception("Autobús no encontrado");
        if (bus.Status.Id == (int)BusStatusEnum.EnServicio )
            throw new Exception("No se puede eliminar un autobús que está en servicio");

        await _busRepository.Delete(bus);
    }

    #endregion

    #region BusSchedule
    
    public async Task CreateBusSchedule(int scheduleId, int busId)
    {
        var bus = await _busRepository.GetByIdAsync(busId, bus => bus.BusType);
        if (bus == null)
            throw new Exception("Autobús no encontrado.");

        var newBusSchedule = new BusSchedule
        {
            BusId = busId,
            ScheduleId = scheduleId,
            AvailableSeats = bus.BusType.SeatingCapacity // Inicializa con la capacidad total del autobús
        };

        await _busScheduleRepository.AddAsync(newBusSchedule);
    }
    #endregion
}