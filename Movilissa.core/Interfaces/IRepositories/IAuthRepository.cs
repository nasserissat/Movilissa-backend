using Microsoft.AspNetCore.Identity;
using Movilissa_api.Models;
using Movilissa.core.DTOs.AUTH;

namespace Movilissa.core.Interfaces;

public interface IAuthRepository
{
    Task<IdentityResult> RegisterUser(User user, string password);
    Task<User> FindUserByEmailAsync(string email);
    Task<(IdentityResult, string)> LoginUser(string email, string password);
    Task<string> GeneratePasswordResetTokenAsync(User user);
    Task<IdentityResult> ResetPasswordAsync(User user, string token, string newPassword);
}
