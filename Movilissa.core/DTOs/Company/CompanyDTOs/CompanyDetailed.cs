using Movilissa.core.DTOs.Shared;

namespace Movilissa.core.DTOs.Company.BranchDTOs;

public record struct CompanyDetailed(
    int Id,
    string Name,
    string Tel,
    string Email,
    string? Logo,
    string? Instagram,
    string? Facebook,
    string? Website,
    double? Score,
    Item Status
);