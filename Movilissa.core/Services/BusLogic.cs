using Movilissa_api.Data.IRepositories;
using Movilissa_api.Models;
using Movilissa.core.DTOs.Bus;

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

    // Obtener un Bus por ID
    public async Task<Bus> GetBusByIdAsync(int busId)
    {
        var bus = await _busRepository.GetByIdAsync(busId);
        if (bus == null)
            throw new Exception("Autobús no encontrado.");

        return bus;
    }

    // Obtener todos los Buses
    public async Task<IEnumerable<Bus>> GetAllBusesAsync()
    {
        return await _busRepository.GetAllAsync();
    }

    // Actualizar un Bus
    public async Task UpdateBusAsync(Bus bus)
    {
        if (bus == null)
            throw new ArgumentNullException(nameof(bus));

        await _busRepository.Update(bus);
    }

    // Eliminar un Bus
    public async Task DeleteBusAsync(int busId)
    {
        var bus = await _busRepository.GetByIdAsync(busId);
        if (bus == null)
            throw new Exception("Autobús no encontrado para eliminar.");

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