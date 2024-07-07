using Movilissa.core.DTOs.Shared;

namespace Movilissa.core.DTOs.Bus;

public record struct BusDetailed(
    int Id,
    string IdentificationNumber,
    string LicensePlate,
    string CompanyName,
    string Model,
    string Brand,
    int SeatingCapacity,
    Item Status,
    List<Item> Amenities
);