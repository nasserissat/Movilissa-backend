namespace Movilissa.core.DTOs.Company.BranchDTOs;

public record struct BranchData(
    string Name,
    double Latitude,
    double Longitude,
    int CompanyId,
    int ProvinceId,
    int Status
);
