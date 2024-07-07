using Movilissa.core.DTOs.Shared;

namespace Movilissa.core.DTOs.Company.BranchDTOs;

public record struct BranchList(
    int Id,
    string Name,
    double Latitude,
    double Longitude,
    Item Province,
    Item Status
);