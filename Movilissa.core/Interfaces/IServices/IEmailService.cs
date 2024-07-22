using SendGrid;

namespace Movilissa.core.Interfaces.IServices;

public interface IEmailService
{
    Task<Response> SendEmailAsync(string email, string subject, string htmlContent);

}