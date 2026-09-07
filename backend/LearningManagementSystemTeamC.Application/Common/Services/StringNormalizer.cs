namespace LearningManagementSystemTeamC.Application.Common.Services;

public static class StringNormalizer
{
    public static string NormalizeEmail(string email)
        => email.Trim().ToLowerInvariant();
}
