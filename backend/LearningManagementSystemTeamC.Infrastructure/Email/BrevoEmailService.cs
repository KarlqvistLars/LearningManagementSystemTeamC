using LearningManagementSystemTeamC.Application.Common.Interfaces;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;

namespace LearningManagementSystemTeamC.Infrastructure.Email;

public class BrevoEmailService : IEmailService
{
    private readonly HttpClient _httpClient;
    private readonly BrevoSettings _settings;

    public BrevoEmailService(
        HttpClient httpClient,
        IOptions<BrevoSettings> settings)
    {
        _httpClient = httpClient;
        _settings = settings.Value;
    }

    public async Task SendPasswordResetAsync(
        string recipient,
        string firstName,
        string resetLink,
        CancellationToken cancellationToken = default)
    {
        var request = new
        {
            to = new[]
            {
                new
                {
                    email = recipient
                }
            },
            templateId = _settings.PasswordResetTemplateId,
            @params = new
            {
                firstName,
                resetLink
            }
        };
        using var response = await _httpClient.PostAsJsonAsync(
            "v3/smtp/email",
            request,
            cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync(
                cancellationToken);

            throw new InvalidOperationException(
                $"Brevo email request failed: {error}");
        }
    }
}