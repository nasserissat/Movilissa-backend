using Movilissa.core.DTOs.Shared;

namespace Movilissa.core.DTOs.Company.BranchDTOs;

public record struct CompanyList(
    int Id,
    string? Logo,
    string Name,
    string Tel,
    string Email,
    int BusQuantity,
    Item Status
);
