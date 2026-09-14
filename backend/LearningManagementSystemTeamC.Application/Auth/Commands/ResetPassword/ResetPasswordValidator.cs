using LearningManagementSystemTeamC.Application.Common.Interfaces;

namespace LearningManagementSystemTeamC.Application.Auth.Commands.ResetPassword;

public class ResetPasswordValidator : IValidator<ResetPasswordCommand>
{
    public Dictionary<string, string[]> Validate(
        ResetPasswordCommand command)
    {
        var errors = new Dictionary<string, string[]>();

        if (string.IsNullOrWhiteSpace(command.Token))
        {
            errors[nameof(command.Token)] = [ResetPasswordRules.ResetTokenRequiredMessage];
        }

        if (string.IsNullOrWhiteSpace(command.NewPassword))
        {
            errors[nameof(command.NewPassword)] = [ResetPasswordRules.NewPasswordRequiredMessage];
        }

        return errors;
    }
}