namespace Movilissa.core.DTOs.Company.BranchDTOs;

public record struct CompanyData
(
    string Name,
    string Tel,
    string Email,
    string Logo,
    string Instagram,
    string Facebook,
    string Website,
    double Score
);