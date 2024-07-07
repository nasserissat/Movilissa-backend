namespace Movilissa.core.DTOs.Company.BranchDTOs;

public record struct CompanyList(
    int Id,
    string? Logo,
    string Name,
    string Tel,
    string Email,
    int BusQuantity,
    ItemDto Status
);
