using LearningManagementSystemTeamC.Domain.Users;

namespace LearningManagementSystemTeamC.Application.Users.Commands.CreateUser;

public class CreateUserValidator
{
    public Dictionary<string, string[]> Validate(
       CreateUserCommand command)
    {
        var errors = new Dictionary<string, string[]>();

        if (string.IsNullOrWhiteSpace(command.Email))
        {
            errors[nameof(command.Email)] =
            [
                UserRules.EmailRequiredMessage
            ];
        }
        else if (command.Email.Length > UserRules.EmailMaxLength)
        {
            errors[nameof(command.Email)] =
            [
                UserRules.EmailTooLongMessage(UserRules.EmailMaxLength)
            ];
        }
        else if (!command.Email.Contains('@'))
        {
            errors[nameof(command.Email)] =
            [
                UserRules.InvalidEmailMessage
            ];
        }

        if (string.IsNullOrWhiteSpace(command.Password))
        {
            errors[nameof(command.Password)] =
            [
                UserRules.PasswordRequiredMessage
            ];
        }
        else if (command.Password.Length > UserRules.PasswordMaxLength)
        {
            errors[nameof(command.Password)] =
            [
                UserRules.PasswordTooLongMessage(UserRules.PasswordMaxLength)
            ];
        }

        if (command.RoleId == Guid.Empty)
        {
            errors[nameof(command.RoleId)] =
            [
                UserRules.RoleRequiredMessage
            ];
        }

        return errors;
    }
}
