using System.Net;
using System.Net.Mail;
using Movilissa.core.Interfaces.IServices;

namespace Movilissa_api.Logic;

public class EmailService : IEmailService
{
    private readonly string _smtpHost;
    private readonly int _smtpPort;
    private readonly string _smtpUser;
    private readonly string _smtpPass;
    
    public EmailService(string smtpHost, int smtpPort, string smtpUser, string smtpPass)
    {
        _smtpHost = smtpHost;
        _smtpPort = smtpPort;
        _smtpUser = smtpUser;
        _smtpPass = smtpPass;
    }
    public async Task SendPasswordResetEmail(string email, string token)
    {
        using (var client = new SmtpClient(_smtpHost)
               {
                   Port = _smtpPort,
                   Credentials = new NetworkCredential(_smtpUser, _smtpPass),
                   EnableSsl = true,
               })
        {
            var mailMessage = new MailMessage
            {
                From = new MailAddress("noreply@yourdomain.com"),
                Subject = "Reset Your Password",
                Body = $"Please reset your password by using this token: {token}",
                IsBodyHtml = true,
            };
            mailMessage.To.Add(email);

            try
            {
                await client.SendMailAsync(mailMessage);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to send email.", ex);
            }
        }
    }
}