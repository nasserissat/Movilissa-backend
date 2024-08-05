using Movilissa.core.DTOs.Shared;

namespace Movilissa.core.DTOs.Bus.AmenityDTOs;

public record struct AmenityData(
    string Name,
    int? Status,
    int CompanyId
);
public record struct AmenityList(
    int Id,
    string Name,
    Item Status
);

public record struct AmenityFilter(
    string Name,
    int statusId
    );