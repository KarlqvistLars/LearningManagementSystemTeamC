using LearningManagementSystemTeamC.Application.Common.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace LearningManagementSystemTeamC.Infrastructure.Security;

public class PasswordResetTokenService : IPasswordResetTokenService
{
    public string GenerateToken()
    {
        var tokenBytes = RandomNumberGenerator.GetBytes(32);

        return Convert.ToBase64String(tokenBytes);
    }

    public string HashToken(string token)
    {
        var tokenBytes = Encoding.UTF8.GetBytes(token);
        var hashBytes = SHA256.HashData(tokenBytes);

        return Convert.ToHexString(hashBytes);
    }
}