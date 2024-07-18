using Microsoft.AspNetCore.Identity;
using Movilissa_api.Models;
using Movilissa.core.DTOs.AUTH;

namespace Movilissa.core.Interfaces;

public interface IAuthRepository
{
    Task<IdentityResult> RegisterUser(User user, string password);
    Task<User> FindUserByEmailAsync(string email);
    Task<IdentityResult> LoginUser(string email, string password);
}
