namespace Movilissa.core.DTOs.Company.BranchDTOs;

public record struct CompanyDetail(
    int Id,
    string Name,
    string Tel,
    string Email,
    string? Logo,
    string? Instagram,
    string? Facebook,
    string? Website,
    double? Score,
    string Status
);