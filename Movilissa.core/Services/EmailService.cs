using System.Net;
using System.Net.Mail;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Movilissa.core.Interfaces.IServices;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Movilissa_api.Logic;

public class EmailService : IEmailService
{
    private readonly SendGridClient _client;
    private readonly EmailAddress _from;

    public EmailService(IOptions<SendGridSettings> settings)
    {
        _client = new SendGridClient(settings.Value.ApiKey);
        _from = new EmailAddress(settings.Value.SenderEmail, settings.Value.SenderName);
    }

    public async Task<Response> SendEmailAsync(string email, string subject, string htmlContent)
    {
        var to = new EmailAddress(email);
        var msg = MailHelper.CreateSingleEmail(_from, to, subject, "", htmlContent);
        var response = await _client.SendEmailAsync(msg);
        return response; 

        // Puedes manejar la respuesta o loguearla según necesites
    }
}

public class SendGridSettings
{
    public string ApiKey { get; set; }
    public string SenderEmail { get; set; }
    public string SenderName { get; set; }
}
