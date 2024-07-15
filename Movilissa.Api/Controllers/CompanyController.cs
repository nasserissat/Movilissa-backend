using Microsoft.AspNetCore.Mvc;
using Movilissa_api.Logic;
using Movilissa_api.Models;
using Movilissa.core.DTOs.Company.BranchDTOs;
using Movilissa.core.Interfaces.IServices;

namespace Movilissa_api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CompanyController : Controller
{
    private readonly IGenericService<User> _userService;
    private readonly ICompanyService _companyService;

    public CompanyController(IGenericService<User> userService, ICompanyService companyService)
    {
        _userService = userService;
        _companyService = companyService;

    }

    public async Task<ActionResult<CompanyList>> GetCompany()
    {
        var authenticated_user_id = 1;
        var user = await _userService.GetById(authenticated_user_id, u => u.Company);
        var company = _companyService.GetCompanyById(user.CompanyId);
        return Ok(company);
    }
} 