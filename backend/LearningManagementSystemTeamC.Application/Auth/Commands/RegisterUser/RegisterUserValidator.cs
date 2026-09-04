using LearningManagementSystemTeamC.Application.Common.Interfaces;
using LearningManagementSystemTeamC.Application.Common.Services;

namespace LearningManagementSystemTeamC.Application.Auth.Commands.RegisterUser;

public class RegisterUserValidator : IValidator<RegisterUserCommand>
{
    public Dictionary<string, string[]> Validate(
    RegisterUserCommand command)
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

        return errors;
    }
}
