using Movilissa_api.Models;
using Movilissa.core.DTOs;
using Movilissa.core.DTOs.Company.BranchDTOs;
using Movilissa.core.Interfaces;

namespace Movilissa_api.Logic;

public class CompanyLogic
{
    private readonly IGenericRepository<Country> _countryRepository;
    private readonly IGenericRepository<Province> _provinceRepository;
    private readonly IGenericRepository<Company> _companyRepository;
    private readonly IGenericRepository<Branch> _branchRepository;
    private readonly IGenericRepository<CompanyStatus> _companyStatusRepository;


    
    public CompanyLogic(IGenericRepository<Country> countryRepository, 
        IGenericRepository<Province> provinceRepository, IGenericRepository<Company> companyRepository, IGenericRepository<Branch> branchRepository,  IGenericRepository<CompanyStatus> companyStatusRepository)
    {
        _countryRepository = countryRepository;
        _provinceRepository = provinceRepository;
        _companyRepository = companyRepository;
        _branchRepository = branchRepository;
        _companyStatusRepository = companyStatusRepository;


    }
    public async  Task<IReadOnlyList<ItemDto>> GetAllCountries()
    {
        var countries = await _countryRepository.GetAllAsync();
        return countries.Select(c => new ItemDto { Id = c.Id, Name = c.Name }).ToList();
    }
    
    public async Task<IReadOnlyList<ItemDto>> GetProvinceList()
    {
        var provinces = await _provinceRepository.GetAllAsync();
        return provinces.Select(p => new ItemDto { Id = p.Id, Name = p.Name }).ToList();
    }
    
    public async Task<IReadOnlyList<ItemDto>> GetCompanyList()
    {
        var companies = await _companyRepository.GetAllAsync();
        return companies.Select(c => new ItemDto { Id = c.Id, Name = c.Name }).ToList();
    }
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
            Status = new ItemDto {Id = c.Status.Id, Name = c.Status.Name}
            
        }).ToList().AsReadOnly();
    }
    
    public async Task<IReadOnlyList<Branch>> GetAllBranchesForCompany(int companyId)
    {
        var branches = await _branchRepository.GetAllAsync(b => b.CompanyId == companyId, b => b.Province);
        return branches.ToList();
    }
    
    public async Task<IReadOnlyList<ItemDto>> GetCompanyStatuses()
    {
        var statuses = await _companyStatusRepository.GetAllAsync();
        return statuses.Select(c => new ItemDto { Id = c.Id, Name = c.Name }).ToList();
    }
    
    
    public async Task<IReadOnlyList<Company>> CreateCompany()
    {
        return await _companyRepository.GetAllAsync();
    }

}