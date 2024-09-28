using System.Net;
using System.Net.Mail;
using AuthorizationServer.Email;
using AuthorizationServer.Interfaces;
using Microsoft.Extensions.Options;

namespace AuthorizationServer.Services;

public class EmailService : IEmailService
{
    public SMTPSettings _smtpSetting { get; }
    public EmailService(IOptions<SMTPSettings> smtpSetting)
    {
        _smtpSetting = smtpSetting.Value;
    }
    public async Task SendEmailAsync(string from, string to, string subject, string body)
    {
        var message = new MailMessage(from, to, subject, body)
        {
            IsBodyHtml = true
        };

        using (var client = new SmtpClient(_smtpSetting.Host, _smtpSetting.Port))
        {
            client.Credentials = new NetworkCredential(_smtpSetting.UserName, _smtpSetting.Password);
            await client.SendMailAsync(message);
        }
    }
}
