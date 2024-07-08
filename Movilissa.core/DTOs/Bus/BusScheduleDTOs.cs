namespace Movilissa.core.DTOs.Bus.AmenityDTOs;

public record struct BusScheduleData(
    int BusId,
    int ScheduleId,
    int AvailableSeats,
    int Status
);

public record struct BusScheduleList(
    int Id,
    string BusIdentificationNumber,
    string ScheduleDescription,
    int AvailableSeats,
    string BusStatus,
    string ScheduleStatus,
    string RouteOrigin,
    string RouteDestinations,
    string BusType,
    int BusSeatingCapacity
);

