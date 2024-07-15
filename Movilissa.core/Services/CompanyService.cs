using Movilissa_api.Enums;
using Movilissa_api.Models;
using Movilissa.core.DTOs;
using Movilissa.core.DTOs.Company.BranchDTOs;
using Movilissa.core.DTOs.Shared;
using Movilissa.core.Interfaces;
using Movilissa.core.Interfaces.IServices;

namespace Movilissa_api.Logic;

public class CompanyService : ICompanyService
{
    private readonly IGenericRepository<Country> _countryRepository;
    private readonly IGenericRepository<Province> _provinceRepository;
    private readonly IGenericRepository<Company> _companyRepository;
    private readonly IGenericRepository<Branch> _branchRepository;
    
    public CompanyService(IGenericRepository<Country> countryRepository, 
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
        var countries = await _countryRepository.GetAll();
        return countries.Select(c => new Item { Id = c.Id, Description = c.Name }).ToList();
    }
    
    public async Task<IReadOnlyList<Item>> GetProvinceList()
    {
        var provinces = await _provinceRepository.GetAll();
        return provinces.Select(p => new Item { Id = p.Id, Description = p.Name }).ToList();
    }
    public async Task<IReadOnlyList<Item>> GetCompanyList()
    {
        var companies = await _companyRepository.GetAll();
        return companies.Select(c => new Item { Id = c.Id, Description = c.Name }).ToList();
    }

    

    #endregion

    #region Company
    public async Task<IReadOnlyList<CompanyList>> GetCompanySummaryList()
    {
        var companies = await _companyRepository.GetAll(null, c => c.Buses, c => c.Status
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
    public async Task<CompanyList> GetCompanyById(int company_id)
    {
        var company = await _companyRepository.GetById(company_id, c => c.Buses, c => c.Status
        );
        if(company == null)
        {
            throw new Exception("Compañía no encontrada.");
        }
        return new CompanyList
        {
            Id = company.Id,
            Logo = company.Logo,
            Name = company.Name,
            Tel = company.Tel,
            Email = company.Email,
            BusQuantity = company.Buses.Count,
            Status = Item.From((GenericStatus)company.Status)

        };
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
        var result =await _companyRepository.Add(newCompany);
        return result.Id;
    }
    public async Task<int> UpdateCompany(int id, CompanyData companyData)
    {
        var company = await _companyRepository.GetById(id);
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
        var company = await _companyRepository.GetById(companyId);
        if (company == null)
            throw new Exception("Compañía no encontrada.");

        company.Status = (int)GenericStatus.Inactivo;

        await _companyRepository.Update(company);
        return company.Id;
    }
    public async Task<int> ActivateCompany(int companyId)
    {
        var company = await _companyRepository.GetById(companyId);
        if (company == null)
            throw new Exception("Compañía no encontrada.");

        company.Status = (int)GenericStatus.Activo;

        await _companyRepository.Update(company);
        return company.Id;
    }
    
    #endregion

    #region Branch
    // Obtener todas las sucursales de una empresa
    public async Task<List<BranchList>> GetAllBranchesForCompany(int companyId)
    {
        var branches = await _branchRepository.GetAll(b => b.CompanyId == companyId, b => b.Province, b => b.Company);
        return branches.Select(b => new BranchList
        {
            Id = b.Id,
            Name = b.Name,
            Address = b.Address,
            Latitude = b.Latitude,
            Longitude = b.Longitude,
            Province = new Item { Id = b.Province.Id, Description = b.Province.Name },
            Status = Item.From((GenericStatus)b.Status)
        }).ToList();
    }

    // Crear una nueva sucursal
    public async Task<int> CreateBranch(BranchData data)
    {
        var newBranch = new Branch
        {
            Name = data.Name,
            Address = data.Address,
            Latitude = data.Latitude,
            Longitude = data.Longitude,
            CompanyId = data.CompanyId,
            ProvinceId = data.ProvinceId,
            Status = (int)GenericStatus.Activo
        };

        await _branchRepository.Add(newBranch);
        return newBranch.Id;
    }

    // Actualizar una sucursal existente
    public async Task<int> UpdateBranch(int branchId, BranchData data)
    {
        var branch = await _branchRepository.GetById(branchId);
        if (branch == null)
            throw new Exception("Sucursal no encontrada.");

        branch.Name = data.Name;
        branch.Address = data.Address;
        branch.Latitude = data.Latitude;
        branch.Longitude = data.Longitude;
        branch.CompanyId = data.CompanyId;
        branch.ProvinceId = data.ProvinceId;
        branch.Status = data.Status;

        await _branchRepository.Update(branch);
        return branch.Id;
    }

    // Desactivar una sucursal
    public async Task<int> InactivateBranch(int branchId)
    {
        var branch = await _branchRepository.GetById(branchId);
        if (branch == null)
            throw new Exception("Sucursal no encontrada.");

        branch.Status = (int)GenericStatus.Inactivo;

        await _branchRepository.Update(branch);
        return branch.Id;
    }

    // Activar una sucursal
    public async Task<int> ActivateBranch(int branchId)
    {
        var branch = await _branchRepository.GetById(branchId);
        if (branch == null)
            throw new Exception("Sucursal no encontrada.");

        branch.Status = (int)GenericStatus.Activo;

        await _branchRepository.Update(branch);
        return branch.Id;
    }
    
    #endregion
    
}