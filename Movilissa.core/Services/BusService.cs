using Movilissa_api.Data.IRepositories;
using Movilissa_api.Enums;
using Movilissa_api.Models;
using Movilissa.core.DTOs.Bus;
using Movilissa.core.DTOs.Bus.AmenityDTOs;
using Movilissa.core.DTOs.Shared;
using Movilissa.core.Interfaces;
using Movilissa.core.Interfaces.IServices;

namespace Movilissa_api.Logic;

public class BusService : IBusService
{
    private readonly IBusRepository _busRepository;
    private readonly IBusScheduleRepository _busScheduleRepository;
    private readonly IGenericRepository<Amenity> _amenityRepository;
    private readonly IGenericRepository<BusType> _busTypeRepository;
    
    public BusService(IBusRepository busRepository, IBusScheduleRepository busScheduleRepository, IGenericRepository<Amenity> amenityRepository, IGenericRepository<BusType> busTypeRepository)
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
        var buses = await _busRepository.GetBusList();
        return buses;
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
            Brand = bus.BusType.Brand.Name,
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
            BrandId = data.BrandId,
            Model = data.Model.Trim(),
            SeatingCapacity = data.Capacity,
            Status = data.StatusId,
            CompanyId = data.CompanyId
        };

        await _busTypeRepository.Add(newBusType);
        return newBusType.Id;
    }

    public async Task<IEnumerable<BusTypeList>> GetAllBusTypes()
    {
        var busTypes = await _busTypeRepository.GetAll();
        var busTypesList = busTypes.Select(bt => new BusTypeList
        {
            Id = bt.Id,
            Brand = new Item { Id = bt.Brand.Id, Description = bt.Brand.Name },
            Model = bt.Model,
            Capacity = bt.SeatingCapacity,
            Status = new Item { Id = bt.Status, Description = ((GenericStatus)bt.Status).ToString() }
        }).ToList();
        return busTypesList;
    }

    public async Task<IEnumerable<BusTypeList>> FilterBusTypes(BusTypeFilter filter)
    {
        var types = await _busRepository.FilterBusTypes(filter);
        return types.Select(bt => new BusTypeList()
        {
            Id = bt.Id,
            Brand = new Item { Id = bt.Brand.Id, Description = bt.Brand.Name },
            Model = bt.Model,
            Capacity = bt.SeatingCapacity,
            Status = new Item { Id = bt.Status, Description = ((GenericStatus)bt.Status).ToString() }
        }).ToList();
    }

    public async Task<int> UpdateBusType(int id, BusTypeData data)
    {
        var busType = await _busTypeRepository.GetById(id);
        if (busType == null)
            throw new Exception("BusType not found.");

        busType.BrandId = data.BrandId;
        busType.Model = data.Model.Trim();
        busType.SeatingCapacity = data.Capacity;
        busType.Status = data.StatusId;

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
    public async Task<IEnumerable<BusScheduleList>> GetAllBusSchedules()
    {
        var schedules = await _busScheduleRepository.GetAllBusSchedulesWithDetails();
        return schedules.Select(s => new BusScheduleList
        {
            Id = s.Id,
            BusIdentificationNumber = s.Bus.IdentificationNumber,
            ScheduleDescription = $"Schedule {s.ScheduleId}",
            AvailableSeats = s.AvailableSeats,
            BusStatus = s.Bus.Status.Name,
            ScheduleStatus = s.Status.ToString(),
            RouteOrigin = s.Schedule.Route.Origin.Name,
            RouteDestinations = string.Join(", ", s.Schedule.Route.Destinations.Select(d => d.Destination.Name)),
            BusType = s.Bus.BusType.Brand + " " + s.Bus.BusType.Model,
            BusSeatingCapacity = s.Bus.BusType.SeatingCapacity
        }).ToList();
    }
    
    public async Task<int> CreateBusSchedule(int scheduleId, int busId)
    {
        var bus = await _busRepository.GetById(busId, bus => bus.BusType);
        if (bus == null)
            throw new Exception("Autobús no encontrado.");

        var schedule = new BusSchedule
        {
            BusId = busId,
            ScheduleId = scheduleId,
            AvailableSeats = bus.BusType.SeatingCapacity,
            Status = BusScheduleStatus.Ready
        };

        await _busScheduleRepository.Add(schedule);
        return schedule.Id;
    }
    // Asignar un horario a un bus
        public async Task AssignScheduleToBus(int busId, int scheduleId)
        {
            var bus = await _busRepository.GetById(busId);
            var schedule = await _busScheduleRepository.GetById(scheduleId);

            if (bus == null || schedule == null)
                throw new Exception("Bus o Horario no encontrado");

            if (bus.StatusId == (int)BusStatusEnum.FueradeServicio)
                throw new Exception("No se puede asignar un horario a un autobús fuera de servicio");

            var busSchedule = new BusSchedule
            {
                BusId = busId,
                ScheduleId = scheduleId,
                AvailableSeats = bus.BusType.SeatingCapacity,
                Status = BusScheduleStatus.Ready
            };

            await _busScheduleRepository.Add(busSchedule);
            bus.StatusId = (int)BusStatusEnum.Reservado; // Cambiar el estado del bus a Reservado
            await _busRepository.Update(bus);
        }

        // Iniciar el trayecto del bus
        public async Task StartBusJourney(int busScheduleId)
        {
            var busSchedule = await _busScheduleRepository.GetById(busScheduleId);
            if (busSchedule == null)
                throw new Exception("Horario del bus no encontrado");

            var bus = await _busRepository.GetById(busSchedule.BusId);
            if (bus == null)
                throw new Exception("Bus no encontrado");

            if (bus.StatusId == (int)BusStatusEnum.FueradeServicio)
                throw new Exception("El bus no puede estar fuera de servicio mientras está en tránsito");

            busSchedule.Status = BusScheduleStatus.InTransit;
            bus.StatusId = (int)BusStatusEnum.EnServicio; // Cambiar el estado del bus a EnServicio
            await _busScheduleRepository.Update(busSchedule);
            await _busRepository.Update(bus);
        }

        // Finalizar el trayecto del bus
        public async Task CompleteBusJourney(int busScheduleId)
        {
            var busSchedule = await _busScheduleRepository.GetById(busScheduleId);
            if (busSchedule == null)
                throw new Exception("Horario del bus no encontrado");

            var bus = await _busRepository.GetById(busSchedule.BusId);
            if (bus == null)
                throw new Exception("Bus no encontrado");

            busSchedule.Status = BusScheduleStatus.Completed;
            bus.StatusId = (int)BusStatusEnum.Disponible; // Cambiar el estado del bus a Disponible
            await _busScheduleRepository.Update(busSchedule);
            await _busRepository.Update(bus);
        }

        // Actualizar la capacidad del bus durante el trayecto
        public async Task UpdateBusCapacity(int busScheduleId, int seatsSold)
        {
            var busSchedule = await _busScheduleRepository.GetById(busScheduleId);
            if (busSchedule == null)
                throw new Exception("Horario del bus no encontrado");

            if (busSchedule.Status != BusScheduleStatus.InTransit && busSchedule.Status != BusScheduleStatus.Ready)
                throw new Exception("No se puede actualizar la capacidad del bus en el estado actual");
            
            busSchedule.AvailableSeats -= seatsSold;
            if (busSchedule.AvailableSeats < 0)
                throw new Exception("No hay suficientes asientos disponibles");

            await _busScheduleRepository.Update(busSchedule);
        }

        // Manejar retrasos
        public async Task DelayBusJourney(int busScheduleId, TimeSpan delay)
        {
            var busSchedule = await _busScheduleRepository.GetById(busScheduleId);
            if (busSchedule == null)
                throw new Exception("Horario del bus no encontrado");

            busSchedule.Status = BusScheduleStatus.Delayed;
            busSchedule.Schedule.ArrivalTime = busSchedule.Schedule.ArrivalTime?.Add(delay);

            await _busScheduleRepository.Update(busSchedule);
        }

        // Cancelar un viaje
        public async Task CancelBusJourney(int busScheduleId)
        {
            var busSchedule = await _busScheduleRepository.GetById(busScheduleId);
            if (busSchedule == null)
                throw new Exception("Horario del bus no encontrado");

            var bus = await _busRepository.GetById(busSchedule.BusId);
            if (bus == null)
                throw new Exception("Bus no encontrado");

            busSchedule.Status = BusScheduleStatus.Cancelled;
            bus.StatusId = (int)BusStatusEnum.Disponible; // Cambiar el estado del bus a Disponible
            await _busScheduleRepository.Update(busSchedule);
            await _busRepository.Update(bus);
        }
        
    public async Task<int> UpdateBusSchedule(int id, BusScheduleData data)
    {
        var schedule = await _busScheduleRepository.GetById(id);
        if (schedule == null)
            throw new Exception("Schedule not found.");

        schedule.AvailableSeats = data.AvailableSeats;
        await _busScheduleRepository.Update(schedule);
        return schedule.Id;
    }
    
    
    #endregion
    
    
    #region Amenity

    public async Task<int> CreateAmenity(AmenityData data)
    {
        var newAmenity = new Amenity
        {
            Name = data.Name.Trim(),
            Status = data.StatusId.Value,
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
    public async Task<IEnumerable<AmenityList>> FilterAmenities( AmenityFilter filter)
    {
        var amenities = await _busRepository.FilterAmenities(filter);
        return amenities.Select(a => new AmenityList
        {
            Id = a.Id,
            Name = a.Name,
            Status = new Item { Id = a.Status, Description = Enum.GetName(typeof(GenericStatus), a.Status) }
        }).ToList();
    }
    #endregion
    
    
    
    

}