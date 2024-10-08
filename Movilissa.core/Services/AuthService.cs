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
        var (result, userName) = await _authRepository.LoginUser(data.Email, data.Password);
        if (result.Succeeded)
            return new UserManagerResponse(true, "Inicio de sesión exitoso", null, null,  userName);

        return new UserManagerResponse(false, "Usuario o contraseña inválido", result.Errors.Select(e => e.Description));
    }
    
    public async Task<UserManagerResponse> GenerateResetPasswordTokenAsync(string email)
    {
        var user = await _authRepository.FindUserByEmailAsync(email);
        if (user == null)
            return new UserManagerResponse(false, "No se encontró un usuario con esa dirección de correo");

        var token = await _authRepository.GeneratePasswordResetTokenAsync(user);
    
        if (string.IsNullOrEmpty(token))
            return new UserManagerResponse(false, "Hubo un fallo en general el token de reseteo de contraseña");

        return new UserManagerResponse(true, "El token para resetear la contrseña se ha generado exitosamente", new string[] { token });
    }
    
    public async Task<UserManagerResponse> ResetPassword(string email, string token, string newPassword)
    {
        var user = await _authRepository.FindUserByEmailAsync(email);
        if (user == null)
        {
            return new UserManagerResponse(false, "No se encontró un usuario con esa dirección de correo");
        }

        var result = await _authRepository.ResetPasswordAsync(user, token, newPassword);
        if (result.Succeeded)
        {
            return new UserManagerResponse(true, "La contraseña ha sido actualizada exitosamente");
        }
        else
        {
            return new UserManagerResponse(false, "Hubo un error al resetar la contraseña", result.Errors.Select(e => e.Description));
        }
    }
    
}
