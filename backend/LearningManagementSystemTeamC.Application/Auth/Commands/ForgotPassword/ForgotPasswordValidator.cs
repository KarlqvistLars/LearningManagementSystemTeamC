using LearningManagementSystemTeamC.Application.Common.Interfaces;

namespace LearningManagementSystemTeamC.Application.Auth.Commands.ForgotPassword;

public class ForgotPasswordValidator : IValidator<ForgotPasswordCommand>
{
    public Dictionary<string, string[]> Validate(
        ForgotPasswordCommand command)
    {
        var errors = new Dictionary<string, string[]>();

        if (string.IsNullOrWhiteSpace(command.Email))
        {
            errors[nameof(command.Email)] =
            [
                ForgotPasswordRules.EmailEmptyMessage
            ];
        }
        return errors;
    }
}