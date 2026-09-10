namespace LearningManagementSystemTeamC.Application.Common.Interfaces;

public interface IPasswordResetTokenService
{
    string GenerateToken();

    string HashToken(string token);
}