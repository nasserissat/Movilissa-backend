using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
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
            return Ok(new { message = response.Message, userName = response.UserName  });

        return BadRequest(new { errors = response.Errors});
    }
    
    [AllowAnonymous]
    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordData data)
    {
        var result = await _authService.GenerateResetPasswordTokenAsync(data.Email);

        if (result.IsSuccess)
        {
              var callbackUrl = Url.Action("ResetPassword", "Auth", new { result.Token, data.Email }, Request.Scheme);
              var message = $"Por favor restablece tu contraseña haciendo clic aquí: <a href='{callbackUrl}'>link</a>";
            await _emailService.SendEmailAsync(data.Email, "Restablecer contraseña", message);
            return Ok(new { message = "Se ha enviado un correo electrónico con instrucciones para restablecer la contraseña." });
        }

        return BadRequest(new { errors = result.Message });
    }

    [AllowAnonymous]
    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordData data)
    {
        var response = await _authService.ResetPassword(data.Email, data.Token, data.NewPassword);
        if (response.IsSuccess)
            return Ok(new { Message = response.Message });

        return BadRequest(new { Errors = response.Errors });
    }


    
    
}