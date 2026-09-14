using LearningManagementSystemTeamC.Application.Common.Interfaces;
using LearningManagementSystemTeamC.Application.Common.Services;

namespace LearningManagementSystemTeamC.Application.Users.Commands.CreateUser;

public class CreateUserValidator : IValidator<CreateUserCommand>
{
    public Dictionary<string, string[]> Validate(
        CreateUserCommand command, CancellationToken cancellationToken)
    {
        var errors = new Dictionary<string, string[]>();

        UserValidation.ValidateEmail(
            command.Email,
            errors,
            nameof(command.Email));

        UserValidation.ValidatePassword(
            command.Password,
            errors,
            nameof(command.Password));

        UserValidation.ValidateRoleId(
            command.RoleId,
            errors,
            nameof(command.RoleId));

        UserValidation.ValidateFirstName(
            command.FirstName,
            errors,
            nameof(command.FirstName));

        UserValidation.ValidateLastName(
            command.LastName,
            errors,
            nameof(command.LastName));

        return errors;
    }
}