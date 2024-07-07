using Movilissa.core.DTOs.Shared;

namespace Movilissa.core.DTOs.Bus;

public record struct BusList(
    int Id,
    string IdentificationNumber,
    string LicensePlate,
    string Model,
    string Brand,
    int SeatingCapacity,
    Item Status
);