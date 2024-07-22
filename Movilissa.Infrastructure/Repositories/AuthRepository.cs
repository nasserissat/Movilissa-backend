using Microsoft.AspNetCore.Identity;
using Movilissa_api.Models;
using Movilissa.core.Interfaces;

namespace Movilissa.Infrastructure.Repositories;

public class AuthRepository : IAuthRepository
{
    private readonly UserManager<User> _userManager;

    public AuthRepository(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<IdentityResult> RegisterUser(User user, string password)
    {
        var result = await _userManager.CreateAsync(user, password);
        return result;
    }

    public async Task<User> FindUserByEmailAsync(string email)
    {
        return await _userManager.FindByEmailAsync(email);
    }

    public async Task<(IdentityResult, string)> LoginUser(string email, string password)
    {
        // Este método supone la existencia de algún mecanismo para verificar las credenciales
        var user = await _userManager.FindByEmailAsync(email);
        if (user != null)
        {
            var result = await _userManager.CheckPasswordAsync(user, password);
            if (result)
                return (IdentityResult.Success, (user.FirstName + " " + user.LastName ));
        } 
        return (IdentityResult.Failed(new IdentityError { Description = "Intento de inicio de sesión inválido" }), null);     
    }
    public async Task<string> GeneratePasswordResetTokenAsync(User user)
    {
        return await _userManager.GeneratePasswordResetTokenAsync(user);
    }

    
    public async Task<IdentityResult> ResetPasswordAsync(User user, string token, string newPassword)
    {
        return await _userManager.ResetPasswordAsync(user, token, newPassword);
    }
}
