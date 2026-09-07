using LearningManagementSystemTeamC.Domain.Users;

namespace LearningManagementSystemTeamC.Application.Common.Services;

public static class UserValidation
{
    public static void ValidateEmail(
            string email,
            Dictionary<string, string[]> errors,
            string propertyName)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            errors[propertyName] =
            [
                UserRules.EmailRequiredMessage
            ];
        }
        else if (email.Length > UserRules.EmailMaxLength)
        {
            errors[propertyName] =
            [
                UserRules.EmailTooLongMessage(UserRules.EmailMaxLength)
            ];
        }
        else if (!email.Contains('@'))
        {
            errors[propertyName] =
            [
                UserRules.InvalidEmailMessage
            ];
        }
    }

    public static void ValidatePassword(
        string password,
        Dictionary<string, string[]> errors,
        string propertyName)
    {
        if (string.IsNullOrWhiteSpace(password))
        {
            errors[propertyName] =
            [
                UserRules.PasswordRequiredMessage
            ];
        }
        else if (password.Length > UserRules.PasswordMaxLength)
        {
            errors[propertyName] =
            [
                UserRules.PasswordTooLongMessage(UserRules.PasswordMaxLength)
            ];
        }
    }

    public static void ValidateRoleId(
    Guid roleId,
    Dictionary<string, string[]> errors,
    string propertyName)
    {
        if (roleId == Guid.Empty)
        {
            errors[propertyName] =
            [
                UserRules.RoleRequiredMessage
            ];
        }
    }
}
