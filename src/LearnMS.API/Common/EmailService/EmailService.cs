
using Microsoft.Extensions.Options;
using MimeKit;

namespace LearnMS.API.Common.EmailService;

public sealed class EmailService(IOptions<EmailConfig> cfg, ILogger<EmailService> logger) : IEmailService
{
    public async Task SendAsync(SendEmailRequest request)
    {
        logger.LogInformation("[EmailService] ==> using config {@cfg}", cfg.Value);

        var email = new MimeMessage();
        var from = request.From is not null ? MailboxAddress.Parse(request.From) : MailboxAddress.Parse(cfg.Value.Sender);
        email.From.Add(from);
        var to = MailboxAddress.Parse(request.To);
        email.Subject = request.Subject;
        email.To.Add(to);
        email.Body = new TextPart("html")
        {
            Text = request.Body
        };

        using var smtp = new MailKit.Net.Smtp.SmtpClient();

        await smtp.ConnectAsync(cfg.Value.Host, cfg.Value.Port, cfg.Value.UseSsl);

        await smtp.AuthenticateAsync(cfg.Value.Username, cfg.Value.Password);

        logger.LogInformation("[EmailService] ==> sending {@subject} email to {@address}", request.Subject, request.To);

        await smtp.SendAsync(email);

        await smtp.DisconnectAsync(true);
    }
}