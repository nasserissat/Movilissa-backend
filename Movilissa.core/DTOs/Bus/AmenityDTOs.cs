namespace Movilissa.core.DTOs.Bus.AmenityDTOs;

public record struct AmenityData(
    string Name,
    int CompanyId
);
public record struct AmenityList(
    int Id,
    string Name
);
