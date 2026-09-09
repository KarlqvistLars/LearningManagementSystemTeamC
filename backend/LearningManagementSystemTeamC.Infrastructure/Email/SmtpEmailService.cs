using LearningManagementSystemTeamC.Application.Common.Interfaces;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MimeKit;

namespace LearningManagementSystemTeamC.Infrastructure.Email;

public class SmtpEmailService : IEmailService
{
    private readonly SmtpSettings _settings;
    private readonly IConfiguration _configuration;

    public SmtpEmailService(
        IOptions<SmtpSettings> settings,
        IConfiguration configuration)
    {
        _settings = settings.Value;
        _configuration = configuration;
    }

    public async Task SendAsync(
        string recipient,
        string subject,
        string body,
        CancellationToken cancellationToken = default)
    {
        var username = _configuration["Smtp:Username"];
        var password = _configuration["Smtp:Password"];

        if (string.IsNullOrWhiteSpace(username))
            throw new InvalidOperationException("SMTP username is not configured.");

        if (string.IsNullOrWhiteSpace(password))
            throw new InvalidOperationException("SMTP password is not configured.");

        var email = new MimeMessage();

        email.From.Add(new MailboxAddress(
            _settings.FromName,
            _settings.FromEmail));

        email.To.Add(MailboxAddress.Parse(recipient));
        email.Subject = subject;

        email.Body = new BodyBuilder
        {
            TextBody = body
        }.ToMessageBody();

        using var smtp = new SmtpClient();

        await smtp.ConnectAsync(
            _settings.Host,
            _settings.Port,
            SecureSocketOptions.StartTls,
            cancellationToken);

        await smtp.AuthenticateAsync(
            username,
            password,
            cancellationToken);

        await smtp.SendAsync(email, cancellationToken);

        await smtp.DisconnectAsync(true, cancellationToken);
    }
}