using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Movilissa.core.DTOs.AUTH;
using Movilissa.core.Responses;

namespace Movilissa.core.Interfaces.IServices;

public interface IAuthService
{
    Task<UserManagerResponse> RegisterUser(RegisterData data);
    Task<UserManagerResponse> LoginUser(LoginData data);
    Task<UserManagerResponse> GenerateResetPasswordTokenAsync(string email);
    Task<UserManagerResponse> ResetPassword(string email, string token, string newPassword);
    

}
