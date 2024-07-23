using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Movilissa_api.Logic;
using Movilissa_api.Models;
using Movilissa.core.DTOs.Company.BranchDTOs;
using Movilissa.core.DTOs.Shared;
using Movilissa.core.Interfaces.IServices;

namespace Movilissa_api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CompanyController : ControllerBase
{
    private readonly IGenericService<User> _userService;
    private readonly ICompanyService _companyService;

    public CompanyController(IGenericService<User> userService, ICompanyService companyService)
    {
        _userService = userService;
        _companyService = companyService;

    }

    [HttpGet("company")]
    public async Task<ActionResult<CompanyList>> GetCompany()
    {
        var authenticated_user_id = 1;
        var user = await _userService.GetById(authenticated_user_id, u => u.Company);
        
        if (user != null && user.CompanyId.HasValue)
        {
            var company = await _companyService.GetCompanyById(user.CompanyId.Value);
            return Ok(company);
        }
        return NotFound("User or Company not found.");
    }
    [AllowAnonymous]

    [HttpGet("companies")]
    public async Task<ActionResult<IReadOnlyList<Item>>> GetCompanies()
    {
        var companies = await _companyService.GetCompanyList();
        return Ok(companies);
    }
    [AllowAnonymous]

    [HttpGet("provinces")]
    public async Task<ActionResult<IReadOnlyList<Item>>> GetProvinces()
    {
        var provinces = await _companyService.GetProvinceList();
        return Ok(provinces);
    }
} 