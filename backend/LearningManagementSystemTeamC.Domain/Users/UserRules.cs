namespace LearningManagementSystemTeamC.Domain.Users;

public class UserRules
{
    public const int EmailMaxLength = 100;
    public const string EmailRequiredCode = "USER_EMAIL_REQUIRED";
    public const string EmailRequiredMessage = "Email is required.";

    public const string RoleRequiredCode = "USER_ROLE_REQUIRED";
    public const string RoleRequiredMessage = "Role is required.";

    public const string EmailRegisteredCode = "USER_EMAIL_REGISTERED";
    public const string EmailRegisteredMessage = "Email is already registered.";

    public const string PasswordRequiredCode = "USER_PASSWORD_REQUIRED";
    public const string PasswordRequiredMessage = "Password is required.";
}
