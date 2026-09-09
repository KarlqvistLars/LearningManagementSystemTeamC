namespace LearningManagementSystemTeamC.Application.Common.Interfaces;

public interface IEmailService
{
    Task SendPasswordResetAsync(
        string recipient,
        string firstName,
        string resetLink,
        CancellationToken cancellationToken = default);
}