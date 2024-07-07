namespace Movilissa.core.DTOs.Bus.AmenityDTOs;

public record struct BusTypeData(
    string Brand,
    string Model,
    int SeatingCapacity,
    int CompanyId
);

public record struct BusTypeList(
    int Id,
    string Brand,
    string Model,
    int SeatingCapacity
);