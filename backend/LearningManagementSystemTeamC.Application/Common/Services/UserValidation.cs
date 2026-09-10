using LearningManagementSystemTeamC.Domain.UserInfos;
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

    public static void ValidateFirstName(
        string firstName,
        Dictionary<string, string[]> errors,
        string propertyName)
    {
        if (string.IsNullOrWhiteSpace(firstName))
        {
            errors[propertyName] =
            [
                UserInfoRules.FirstNameRequiredMessage
            ];
        }
        else if (firstName.Length > UserInfoRules.FirstNameMaxLength)
        {
            errors[propertyName] =
            [
                UserInfoRules.FirstNameTooLongMessage(
                    UserInfoRules.FirstNameMaxLength)
            ];
        }
    }

    public static void ValidateLastName(
        string lastName,
        Dictionary<string, string[]> errors,
        string propertyName)
    {
        if (string.IsNullOrWhiteSpace(lastName))
        {
            errors[propertyName] =
            [
                UserInfoRules.LastNameRequiredMessage
            ];
        }
        else if (lastName.Length > UserInfoRules.LastNameMaxLength)
        {
            errors[propertyName] =
            [
                UserInfoRules.LastNameTooLongMessage(
                    UserInfoRules.LastNameMaxLength)
            ];
        }
    }
}