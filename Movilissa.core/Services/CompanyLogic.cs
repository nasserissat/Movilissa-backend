using Movilissa_api.Enums;
using Movilissa_api.Models;
using Movilissa.core.DTOs;
using Movilissa.core.DTOs.Company.BranchDTOs;
using Movilissa.core.DTOs.Shared;
using Movilissa.core.Interfaces;

namespace Movilissa_api.Logic;

public class CompanyLogic
{
    private readonly IGenericRepository<Country> _countryRepository;
    private readonly IGenericRepository<Province> _provinceRepository;
    private readonly IGenericRepository<Company> _companyRepository;
    private readonly IGenericRepository<Branch> _branchRepository;


    
    public CompanyLogic(IGenericRepository<Country> countryRepository, 
        IGenericRepository<Province> provinceRepository, IGenericRepository<Company> companyRepository, IGenericRepository<Branch> branchRepository)
    {
        _countryRepository = countryRepository;
        _provinceRepository = provinceRepository;
        _companyRepository = companyRepository;
        _branchRepository = branchRepository;
    }

    #region Configuration
    public async  Task<IReadOnlyList<Item>> GetAllCountries()
    {
        var countries = await _countryRepository.GetAllAsync();
        return countries.Select(c => new Item { Id = c.Id, Description = c.Name }).ToList();
    }
    
    public async Task<IReadOnlyList<Item>> GetProvinceList()
    {
        var provinces = await _provinceRepository.GetAllAsync();
        return provinces.Select(p => new Item { Id = p.Id, Description = p.Name }).ToList();
    }
    public async Task<IReadOnlyList<Item>> GetCompanyList()
    {
        var companies = await _companyRepository.GetAllAsync();
        return companies.Select(c => new Item { Id = c.Id, Description = c.Name }).ToList();
    }

    

    #endregion

    #region Company
    public async Task<IReadOnlyList<CompanyList>> GetCompanySummaryList()
    {
        var companies = await _companyRepository.GetAllAsync(null, c => c.Buses, c => c.Status
        );        return companies.Select(c => new CompanyList 
        {
            Id = c.Id,
            Logo = c.Logo,
            Name = c.Name,
            Tel = c.Tel,
            Email = c.Email,
            BusQuantity = c.Buses.Count,
            Status = Item.From((GenericStatus)c.Status)
            
        }).ToList().AsReadOnly();
    }
    
    public async Task<int> CreateCompany(CompanyData data)
    {
        var newCompany = new Company
        {
            Name = data.Name,
            Tel = data.Tel,
            Email = data.Email,
            Logo = data.Logo,
            Instagram = data.Instagram,
            Facebook = data.Facebook,
            Website = data.Website,
            Score = data.Score,
            Status = (int)GenericStatus.Activo
        };
        var result =await _companyRepository.AddAsync(newCompany);
        return result.Id;
    }
    public async Task<int> UpdateCompany(int id, CompanyData companyData)
    {
        var company = await _companyRepository.GetByIdAsync(id);
        if (company == null)
            throw new Exception("Compañía no encontrada.");

        company.Name = companyData.Name;
        company.Tel = companyData.Tel;
        company.Email = companyData.Email;
        company.Logo = companyData.Logo;
        company.Instagram = companyData.Instagram;
        company.Facebook = companyData.Facebook;
        company.Website = companyData.Website;
        company.Score = companyData.Score;

       await _companyRepository.Update(company);
       return company.Id;
    }
    public async Task<int> InactivateCompany(int companyId)
    {
        var company = await _companyRepository.GetByIdAsync(companyId);
        if (company == null)
            throw new Exception("Compañía no encontrada.");

        company.Status = (int)GenericStatus.Inactivo;

        await _companyRepository.Update(company);
        return company.Id;
    }
    public async Task<int> ActivateCompany(int companyId)
    {
        var company = await _companyRepository.GetByIdAsync(companyId);
        if (company == null)
            throw new Exception("Compañía no encontrada.");

        company.Status = (int)GenericStatus.Activo;

        await _companyRepository.Update(company);
        return company.Id;
    }



    
    #endregion

    #region Branch
    public async Task<IReadOnlyList<Branch>> GetAllBranchesForCompany(int companyId)
    {
        var branches = await _branchRepository.GetAllAsync(b => b.CompanyId == companyId, b => b.Province);
        return branches.ToList();
    }
    
    #endregion
    
}