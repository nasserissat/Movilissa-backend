using Movilissa_api.Data.IRepositories;
using Movilissa_api.Enums;
using Movilissa_api.Models;
using Movilissa.core.DTOs.Bus;
using Movilissa.core.DTOs.Bus.AmenityDTOs;
using Movilissa.core.DTOs.Shared;
using Movilissa.core.Interfaces;

namespace Movilissa_api.Logic;

public class BusLogic
{
    private readonly IBusRepository _busRepository;
    private readonly IBusScheduleRepository _busScheduleRepository;
    private readonly IGenericRepository<Amenity> _amenityRepository;
    private readonly IGenericRepository<BusType> _busTypeRepository;



    public BusLogic(IBusRepository busRepository, IBusScheduleRepository busScheduleRepository, IGenericRepository<Amenity> amenityRepository, IGenericRepository<BusType> busTypeRepository)
    {
        _busRepository = busRepository;
        _busScheduleRepository = busScheduleRepository;
        _amenityRepository = amenityRepository;
        _busTypeRepository = busTypeRepository;

    }

    #region Bus
    
    // Obtener todos los buses
    public async Task<IEnumerable<BusList>> GetAllBuses()
    {
        var buses = await _busRepository.GetAll(null, b => b.Company, b => b.BusType);
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
    public async Task<int> CreateBus(BusData busDTO)
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

        await _busRepository.Add(bus);
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
    
    public async Task UpdateBus(int id, BusData busData)
    {
        var bus = await _busRepository.GetById(id);
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
    public async Task DeleteBus(int id)
    {
        var bus = await _busRepository.GetById(id);
        if (bus == null)
            throw new Exception("Autobús no encontrado");
        if (bus.Status.Id == (int)BusStatusEnum.EnServicio )
            throw new Exception("No se puede eliminar un autobús que está en servicio");

        await _busRepository.Delete(bus);
    }

    #endregion
    
    #region BusType

    public async Task<int> CreateBusType(BusTypeData data)
    {
        var newBusType = new BusType
        {
            Brand = data.Brand.Trim(),
            Model = data.Model.Trim(),
            SeatingCapacity = data.SeatingCapacity,
            CompanyId = data.CompanyId
        };

        await _busTypeRepository.Add(newBusType);
        return newBusType.Id;
    }

    public async Task<IEnumerable<BusTypeList>> GetAllBusTypes()
    {
        var busTypes = await _busTypeRepository.GetAll();
        return busTypes.Select(bt => new BusTypeList
        {
            Id = bt.Id,
            Brand = bt.Brand,
            Model = bt.Model,
            SeatingCapacity = bt.SeatingCapacity
        }).ToList();
    }

    public async Task<int> UpdateBusType(int id, BusTypeData data)
    {
        var busType = await _busTypeRepository.GetById(id);
        if (busType == null)
            throw new Exception("BusType not found.");

        busType.Brand = data.Brand.Trim();
        busType.Model = data.Model.Trim();
        busType.SeatingCapacity = data.SeatingCapacity;
        busType.CompanyId = data.CompanyId;

        await _busTypeRepository.Update(busType);
        return busType.Id;
    }

    public async Task<int> DeleteBusType(int id)
    {
        var busType = await _busTypeRepository.GetById(id);
        if (busType == null)
            throw new Exception("BusType not found.");

        await _busTypeRepository.Delete(busType);
        return busType.Id;
    }

    #endregion


    #region BusSchedule
    
    public async Task CreateBusSchedule(int scheduleId, int busId)
    {
        var bus = await _busRepository.GetById(busId, bus => bus.BusType);
        if (bus == null)
            throw new Exception("Autobús no encontrado.");

        var newBusSchedule = new BusSchedule
        {
            BusId = busId,
            ScheduleId = scheduleId,
            AvailableSeats = bus.BusType.SeatingCapacity // Inicializa con la capacidad total del autobús
        };

        await _busScheduleRepository.Add(newBusSchedule);
    }
    #endregion
    
    
    #region Amenity

    public async Task<int> CreateAmenity(AmenityData data)
    {
        var newAmenity = new Amenity
        {
            Name = data.Name.Trim(),
            CompanyId = data.CompanyId
        };

        await _amenityRepository.Add(newAmenity);
        return newAmenity.Id;
    }

    public async Task<IEnumerable<AmenityList>> GetAllAmenities()
    {
        var amenities = await _amenityRepository.GetAll();
        return amenities.Select(a => new AmenityList
        {
            Id = a.Id,
            Name = a.Name
        }).ToList();
    }

    public async Task<int> UpdateAmenity(int id, AmenityData data)
    {
        var amenity = await _amenityRepository.GetById(id);
        if (amenity == null)
            throw new Exception("Amenity not found.");

        amenity.Name = data.Name.Trim();
        amenity.CompanyId = data.CompanyId;

        await _amenityRepository.Update(amenity);
        return amenity.Id;
    }

    public async Task<int> DeleteAmenity(int id)
    {
        var amenity = await _amenityRepository.GetById(id);
        if (amenity == null)
            throw new Exception("Amenity not found.");

        await _amenityRepository.Delete(amenity);
        return amenity.Id;
    }

    #endregion
    
    
    
    

}