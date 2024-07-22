using Microsoft.AspNetCore.Identity;
using Movilissa_api.Models;
using Movilissa.core.DTOs.AUTH;
using Movilissa.core.Interfaces;
using Movilissa.core.Interfaces.IServices;
using Movilissa.core.Responses;

namespace Movilissa_api.Logic;

public class AuthService : IAuthService
{
    private readonly IAuthRepository _authRepository;

    public AuthService(IAuthRepository authRepository)
    {
        _authRepository = authRepository;
    }

    public async Task<UserManagerResponse> RegisterUser(RegisterData data)
    {
        if (data == null)
        {
            return new UserManagerResponse(false, "Registro nulo", new string[] { "No se proporcionaron datos." });
        }

        if (data.Password != data.ConfirmPassword)
            return new UserManagerResponse(false, "La contraseña no coincide con la confirmación.");

                
        var user = new User
        {
            UserName = data.Email,
            Email = data.Email,
            FirstName = data.FirstName,
            LastName = data.LastName,
            PasswordHash = data.Password,
            CompanyId = data.CompanyId
        };

        var result = await _authRepository.RegisterUser(user, data.Password);
        if (result.Succeeded)
        {
            return new UserManagerResponse(true, "Usuario registrado con éxito");
        }
        else
        {
            return new UserManagerResponse(false, "Error al registrar usuario", result.Errors.Select(e => e.Description));
        }

    }
    public async Task<UserManagerResponse> LoginUser(LoginData data)
    {
        var result = await _authRepository.LoginUser(data.Email, data.Password);
        if (result.Succeeded)
            return new UserManagerResponse(true, "Inicio de sesión exitoso");

        return new UserManagerResponse(false, "Intento de inicio de sesión inválido", result.Errors.Select(e => e.Description));
    }
    
    public async Task<UserManagerResponse> GenerateResetPasswordTokenAsync(string email)
    {
        var user = await _authRepository.FindUserByEmailAsync(email);
        if (user == null)
            return new UserManagerResponse(false, "No user found with that email address.");

        var token = await _authRepository.GeneratePasswordResetTokenAsync(user);
    
        if (string.IsNullOrEmpty(token))
            return new UserManagerResponse(false, "Failed to generate password reset token.");

        return new UserManagerResponse(true, "Password reset token generated successfully.", new string[] { token });
    }
    
    public async Task<UserManagerResponse> ResetPassword(string email, string token, string newPassword)
    {
        var user = await _authRepository.FindUserByEmailAsync(email);
        if (user == null)
        {
            return new UserManagerResponse(false, "No user found with that email address.");
        }

        var result = await _authRepository.ResetPasswordAsync(user, token, newPassword);
        if (result.Succeeded)
        {
            return new UserManagerResponse(true, "Password reset successfully.");
        }
        else
        {
            return new UserManagerResponse(false, "Failed to reset password", result.Errors.Select(e => e.Description));
        }
    }
    
}
