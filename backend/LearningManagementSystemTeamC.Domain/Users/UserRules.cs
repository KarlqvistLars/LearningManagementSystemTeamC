namespace LearningManagementSystemTeamC.Domain.Users;

public class UserRules
{
    public const int EmailMaxLength = 100;
    public const int PasswordMaxLength = 20;
    public const string EmailRequiredCode = "USER_EMAIL_REQUIRED";
    public const string EmailRequiredMessage = "Email is required.";

    public const string RoleRequiredCode = "USER_ROLE_REQUIRED";
    public const string RoleRequiredMessage = "Role is required.";

    public const string EmailRegisteredCode = "USER_EMAIL_REGISTERED";
    public const string EmailRegisteredMessage = "Email is already registered.";

    public const string PasswordRequiredCode = "USER_PASSWORD_REQUIRED";
    public const string PasswordRequiredMessage = "Password is required.";

    public const string InvalidEmailMessage = "Invalid email format.";

    public const string PasswordTooLongCode = "USER_PASSWORD_TOO_LONG";
    public static string PasswordTooLongMessage(int maxLength) =>
         $"Password is too long. Maximum length can not exceed {maxLength} characters.";

    public const string EmailTooLongCode = "USER_EMAIL_TOO_LONG";
    public static string EmailTooLongMessage(int maxLength) =>
         $"Email is too long. Maximum length can not exceed {maxLength} characters.";

}
