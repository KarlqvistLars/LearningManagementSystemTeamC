using LearningManagementSystemTeamC.Domain.Common.Exceptions;

namespace LearningManagementSystemTeamC.Domain.Users;

public class User
{
    public Guid Id { get; private set; }
    public string Email { get; private set; } = string.Empty;
    public Guid RoleId { get; private set; }
    public bool IsActive { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public void Disable() => IsActive = false;

    public void Enable() => IsActive = true;

    private User() { }

    public User(string email, Guid roleId)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new DomainException(UserRules.EmailRequiredCode, UserRules.EmailRequiredMessage);

        if (roleId == Guid.Empty)
            throw new DomainException(UserRules.RoleRequiredCode, UserRules.RoleRequiredMessage);

        Id = Guid.NewGuid();
        Email = email;
        RoleId = roleId;
        IsActive = true;
        CreatedAt = DateTime.UtcNow;
    }
}
