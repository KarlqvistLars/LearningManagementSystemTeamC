using LearningManagementSystemTeamC.Domain.Common.Exceptions;

namespace LearningManagementSystemTeamC.Domain.Users;

public class User
{
    public Guid Id { get; private set; }
    public string Email { get; private set; } = string.Empty;
    public Guid RoleId { get; private set; }
    public bool IsActive { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public string PasswordHash { get; private set; } = string.Empty;

    public void Disable() => IsActive = false;

    public void Enable() => IsActive = true;

    private User() { }

    public User(string email, string passwordHash, Guid roleId)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new DomainException(UserRules.EmailRequiredCode, UserRules.EmailRequiredMessage);
        if (email.Length > UserRules.EmailMaxLength)
            throw new DomainException(
                UserRules.EmailTooLongCode,
                UserRules.EmailTooLongMessage(UserRules.EmailMaxLength));

        if (string.IsNullOrWhiteSpace(passwordHash))
            throw new DomainException(UserRules.PasswordRequiredCode, UserRules.PasswordRequiredMessage);

        if (roleId == Guid.Empty)
            throw new DomainException(UserRules.RoleRequiredCode, UserRules.RoleRequiredMessage);

        Id = Guid.NewGuid();
        Email = email;
        PasswordHash = passwordHash;
        RoleId = roleId;
        IsActive = true;
        CreatedAt = DateTime.UtcNow;
    }
}
