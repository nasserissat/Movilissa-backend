namespace Movilissa.core.DTOs.Bus;

public record struct BusData(
    string IdentificationNumber,
    string LicensePlate,
    int CompanyId,
    int BusTypeId,
    List<int> AmenityIds
);