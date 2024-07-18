namespace Movilissa.core.Interfaces.IServices;

public interface IEmailService
{
    Task SendPasswordResetEmail(string email, string token);
}