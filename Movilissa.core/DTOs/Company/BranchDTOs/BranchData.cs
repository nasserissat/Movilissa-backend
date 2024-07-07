namespace Movilissa.core.DTOs.Company.BranchDTOs;

public record struct BranchData(
    string Name,
    string Address,
    double Latitude,
    double Longitude,
    int CompanyId,
    int ProvinceId,
    int Status
);
