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
    private readonly IEmailService _emailService;

    public AuthController(IAuthService authService, IEmailService emailService)
    {
        _authService = authService;
        _emailService = emailService;

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
    [AllowAnonymous]

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginData data)
    {
        var response = await _authService.LoginUser(data);

        if (response.IsSuccess)
            return Ok(new { message = response.Message });

        return BadRequest(new { errors = response.Errors });
    }
    
    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPassword(string email)
    {
        var result = await _authService.GenerateResetPasswordTokenAsync(email);

        if (result.IsSuccess)
        {
            // Enviar correo con el token
            await _emailService.SendPasswordResetEmail(email, result.Token);
            return Ok(new { message = "Se ha enviado un correo electrónico con instrucciones para restablecer la contraseña." });
        }

        return BadRequest(new { errors = result.Message });
    }

    
    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordData data)
    {
        var response = await _authService.ResetPassword(data.Email, data.Token, data.NewPassword);
        if (response.IsSuccess)
            return Ok(new { Message = response.Message });

        return BadRequest(new { Errors = response.Errors });
    }


    
    
}