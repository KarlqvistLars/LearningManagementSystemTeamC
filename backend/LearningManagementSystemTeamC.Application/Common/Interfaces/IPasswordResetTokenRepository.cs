using LearningManagementSystemTeamC.Domain.PasswordResetTokens;

namespace LearningManagementSystemTeamC.Application.Common.Interfaces;

public interface IPasswordResetTokenRepository
{
    Task AddAsync(
        PasswordResetToken token,
        CancellationToken cancellationToken = default);

    Task<PasswordResetToken?> GetByTokenHashAsync(
        string tokenHash,
        CancellationToken cancellationToken = default);
}