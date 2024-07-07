namespace Movilissa.core.DTOs.Company.BranchDTOs;

public record struct BranchDetail(
    int Id,
    string Name,
    double Latitude,
    double Longitude,
    int ProvinceId,
    string ProvinceName,
    CompanyDetail Company
);