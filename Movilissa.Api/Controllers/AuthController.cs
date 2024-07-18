using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Movilissa_api.Logic;
using Movilissa_api.Models;
using Movilissa.core.DTOs.AUTH;
using Movilissa.core.Interfaces.IServices;

namespace Movilissa_api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : Controller
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }
    
    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterData data)
    {
        var response = await _authService.RegisterUser(data);

        if (response.IsSuccess)
            return Ok(new { message = response.Message });

        return BadRequest(new { errors = response.Errors });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginData data)
    {
        var response = await _authService.LoginUser(data);

        if (response.IsSuccess)
            return Ok(new { message = response.Message });

        return BadRequest(new { errors = response.Errors });
    }
    
    
}