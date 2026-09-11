namespace LearningManagementSystemTeamC.Domain.PasswordResetTokens;

public class PasswordResetToken
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public string TokenHash { get; private set; } = string.Empty;
    public DateTime ExpiresAt { get; private set; }
    public DateTime? UsedAt { get; private set; }

    private PasswordResetToken()
    {
    }

    public PasswordResetToken(
        Guid userId,
        string tokenHash,
        DateTime expiresAt)
    {
        Id = Guid.NewGuid();
        UserId = userId;
        TokenHash = tokenHash;
        ExpiresAt = expiresAt;
    }

    public bool IsValid()
    {
        return UsedAt == null && DateTime.UtcNow < ExpiresAt;
    }

    public void MarkAsUsed()
    {
        UsedAt = DateTime.UtcNow;
    }
}