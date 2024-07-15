using Movilissa_api.Models;
using Movilissa.core.DTOs.Company.BranchDTOs;
using Movilissa.core.DTOs.Shared;

namespace Movilissa.core.Interfaces.IServices;

public interface ICompanyService
{
    Task<IReadOnlyList<Item>> GetAllCountries();
    Task<IReadOnlyList<Item>> GetProvinceList();
    Task<IReadOnlyList<Item>> GetCompanyList();
    Task<IReadOnlyList<CompanyList>> GetCompanySummaryList();
    Task<CompanyList> GetCompanyById(int id);
    Task<int> CreateCompany(CompanyData data);
    Task<int> UpdateCompany(int id, CompanyData companyData);
    Task<int> InactivateCompany(int companyId);
    Task<int> ActivateCompany(int companyId);
    Task<List<BranchList>> GetAllBranchesForCompany(int companyId);
    Task<int> CreateBranch(BranchData data);
    Task<int> UpdateBranch(int branchId, BranchData data);
    Task<int> InactivateBranch(int branchId);
    Task<int> ActivateBranch(int branchId);
}